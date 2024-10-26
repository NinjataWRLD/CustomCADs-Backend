#pragma warning disable IDE0130
using CustomCADs.Auth.Endpoints.Helpers;
using CustomCADs.Auth.Infrastructure;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Infrastructure.Email;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wolverine;
using static CustomCADs.Shared.Domain.Constants;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthEndpoints(this IServiceCollection services, IConfiguration config)
        => services
            .AddIdentityAuth()
            .AddMessageBus()
            .AddEmail(config)
            .AddEndpoints()
            .AddApiDocumentation()
            .AddAuthAndJwt(config)
            .AddRoles([Client, Contributor, Designer, Admin]);
    
    public static IApplicationBuilder UseAuthEndpoints(this IApplicationBuilder app)
        => app
            .UseGlobalExceptionHandler()
            .UseEndpoints();

    private static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        services.AddWolverine(cfg => { });
        
        return services;
    }

    private static IServiceCollection AddIdentityAuth(this IServiceCollection services)
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

    private static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailOptions>(config.GetSection("Email"));
        services.AddEmailServices();

        return services;
    }

    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddEndpointsApiExplorer();

        return services;
    }

    private static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "CustomCADs Auth API",
                Description = "An API for AuthN/AuthZ related operations.",
                Contact = new() { Name = "Ivan", Email = "ivanangelov414@gmail.com" },
                License = new() { Name = "Apache License 2.0", Url = new("https://www.apache.org/licenses/LICENSE-2.0") },
                Version = "v1"
            });
        });

        return services;
    }

    private static IServiceCollection AddAuthAndJwt(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
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

        return services;
    }

    private static IServiceCollection AddRoles(this IServiceCollection services, IEnumerable<string> roles)
    {
        services.AddAuthorization(options =>
        {
            foreach (string role in roles)
            {
                options.AddPolicy(role, policy => policy.RequireRole(role));
            }
        });

        return services;
    }

    private static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var ehf = context.Features.Get<IExceptionHandlerFeature>();
                var ex = ehf?.Error;

                if (ex != null)
                {
                    await GlobalExceptionHandler
                        .TryHandleAsync(context, ex, context.RequestAborted)
                        .ConfigureAwait(false);
                }
            });
        });

        return app;
    }

    private static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        app.UseFastEndpoints(cfg =>
        {
            cfg.Endpoints.RoutePrefix = "API";
            cfg.Versioning.DefaultVersion = 1;
            cfg.Versioning.PrependToRoute = true;
        });

        return app;
    }
}
