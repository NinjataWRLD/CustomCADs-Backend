using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

	public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder, (string SecretKey, string Issuer, string Audience) settings)
	{
		builder.AddJwtBearer(opt =>
		{
			opt.TokenValidationParameters = new()
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = settings.Issuer,
				ValidAudience = settings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(
					key: Encoding.UTF8.GetBytes(settings.SecretKey)
				),
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
			if (!string.IsNullOrWhiteSpace(accessToken))
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

	public static IApplicationBuilder UseCsrfProtection(this IApplicationBuilder app)
	{
		app.Use(async (context, next) =>
		{
			if (
				context.Request.IsMutationBySpec() // might mutate state
				&& context.User.GetAuthentication() == true // has access to sensitive info
				&& context.Request.IsCsrfVulnerable() // no csrf protection
			)
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
			await next().ConfigureAwait(false);
		});

		return app;
	}

	private static bool IsCsrfVulnerable(this HttpRequest Request)
	{
		string? cookie = Request.Cookies["csrf"];
		string? header = Request.Headers["Csrf-Token"];
		return string.IsNullOrEmpty(cookie) || string.IsNullOrEmpty(header) || cookie != header;
	}
}
