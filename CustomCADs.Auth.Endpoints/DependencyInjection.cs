using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        => services
            .AddAuthInfrastructure(config)
            .AddAuthApplication(config)
            .AddAppIdentity();

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        => app.UseGlobalExceptionHandler();

    private static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddEntityFrameworkStores<AuthContext>()
        .AddDefaultTokenProviders();

        return services;
    }

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
             };
         });

        return builder;
    }

    private static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var ehf = context.Features.Get<IExceptionHandlerFeature>();
                var ex = ehf?.Error;

                if (ex is not null)
                {
                    await GlobalExceptionHandler
                        .TryHandleAsync(context, ex, context.RequestAborted)
                        .ConfigureAwait(false);
                }
            });
        });

        return app;
    }
}
