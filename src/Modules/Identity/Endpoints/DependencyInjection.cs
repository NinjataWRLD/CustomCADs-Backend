using CustomCADs.Shared.Core.Common.Exceptions.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

	public static AuthenticationBuilder AddAuthN(this IServiceCollection services, string scheme = AuthScheme)
	{
		AuthenticationBuilder builder = services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = scheme;
			opt.DefaultForbidScheme = scheme;
			opt.DefaultSignInScheme = scheme;
			opt.DefaultSignOutScheme = scheme;
			opt.DefaultChallengeScheme = scheme;
			opt.DefaultScheme = scheme;
		});

		return builder;
	}

	public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder, IConfiguration config)
	{
		static (string SecretKey, string Issuer, string Audience) ExtractJwt(IConfiguration config)
		{
			var section = config.GetSection("Jwt");

			string? secretKey = section["SecretKey"];
			ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));
			string? issuer = section["Issuer"];
			ArgumentNullException.ThrowIfNull(issuer, nameof(issuer));
			string? audience = section["Audience"];
			ArgumentNullException.ThrowIfNull(audience, nameof(audience));

			return (
				SecretKey: secretKey,
				Issuer: issuer,
				Audience: audience
			);
		}

		builder.AddJwtBearer(opt =>
		{
			var (SecretKey, Issuer, Audience) = ExtractJwt(config);
			opt.TokenValidationParameters = new()
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = Issuer,
				ValidAudience = Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
			};

			opt.Events = new()
			{
				OnMessageReceived = context =>
				{
					context.Token = context.Request.Cookies["jwt"];
					return Task.CompletedTask;
				},

				OnChallenge = async context =>
				{
					context.HandleResponse();

					await context.HttpContext.RequestServices
					   .GetRequiredService<IProblemDetailsService>()
					   .UnauthorizedResponseAsync(
							context: context.HttpContext,
							ex: new UnauthorizedAccessException()
					   ).ConfigureAwait(false);
				},

				OnForbidden = async context =>
				{
					await context.HttpContext.RequestServices
					   .GetRequiredService<IProblemDetailsService>()
					   .ForbiddenResponseAsync(
							context: context.HttpContext,
							ex: new AccessViolationException()
					   ).ConfigureAwait(false);
				},

				OnTokenValidated = context =>
				{
					ClaimsIdentity claimsIdentity = new(context.Principal?.Claims ?? [], AuthScheme);
					context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
					return Task.CompletedTask;
				},
			};
		});

		return builder;
	}

	public static IApplicationBuilder UseJwtPrincipal(this IApplicationBuilder app)
	{
		app.Use(async (context, next) =>
		{
			string? accessToken = context.Request.Cookies["jwt"];
			if (accessToken is not null)
			{
				if (new JwtSecurityTokenHandler().ReadToken(accessToken) is JwtSecurityToken jwt)
				{
					ClaimsIdentity identity = new(jwt.Claims, AuthScheme);
					context.User = new(identity);
				}
			}

			await next().ConfigureAwait(false);
		});

		return app;
	}

	private static bool IsSensitive(this HttpRequest request) =>
		HttpMethods.IsPost(request.Method) ||
		HttpMethods.IsPut(request.Method) ||
		HttpMethods.IsPatch(request.Method) ||
		HttpMethods.IsDelete(request.Method);

	public static IApplicationBuilder UseCsrfProtection(this IApplicationBuilder app)
	{
		app.Use(async (context, next) =>
		{
			if (context.Request.IsSensitive() && context.User.GetAuthentication())
			{
				string? csrfCookie = context.Request.Cookies["csrf"];
				string? csrfHeader = context.Request.Headers["Csrf-Token"];
				if (IsCsrfVulnerable(csrfCookie, csrfHeader))
				{
					await context.RequestServices
					   .GetRequiredService<IProblemDetailsService>()
					   .ForbiddenResponseAsync(
							context: context,
							ex: new CustomException("CSRF token validation failed: cookie and header mismatch."),
							message: "CSRF token mismatch."
						).ConfigureAwait(false);
					return;
				}

			}
			await next().ConfigureAwait(false);
		});

		static bool IsCsrfVulnerable(string? cookie, string? header)
			=> string.IsNullOrEmpty(cookie)
			|| string.IsNullOrEmpty(header)
			|| cookie != header;

		return app;
	}
}
