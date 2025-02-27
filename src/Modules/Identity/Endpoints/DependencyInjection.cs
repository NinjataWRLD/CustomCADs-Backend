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

    public static IServiceCollection AddIdentityExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<IdentityExceptionHandler>();

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
        builder.AddJwtBearer(opt =>
         {
             string? secretKey = config["JwtSettings:SecretKey"];
             ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

             opt.TokenValidationParameters = new()
             {
                 ValidateAudience = true,
                 ValidateIssuer = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = config["JwtSettings:Issuer"],
                 ValidAudience = config["JwtSettings:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
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
                             ex: new UnauthorizedAccessException())
                        .ConfigureAwait(false);
                 },

                 OnForbidden = async context =>
                 {
                     await context.HttpContext.RequestServices
                        .GetRequiredService<IProblemDetailsService>()
                        .ForbiddenResponseAsync(
                             context: context.HttpContext,
                             ex: new AccessViolationException())
                        .ConfigureAwait(false);
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
                JwtSecurityTokenHandler handler = new();
                if (handler.ReadToken(accessToken) is JwtSecurityToken jwt)
                {
                    ClaimsIdentity identity = new(jwt.Claims, AuthScheme);
                    context.User = new(identity);
                }
            }

            await next().ConfigureAwait(false);
        });

        return app;
    }
}
