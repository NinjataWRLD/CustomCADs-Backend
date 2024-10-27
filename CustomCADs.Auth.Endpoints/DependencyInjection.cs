﻿#pragma warning disable IDE0130
using CustomCADs.Auth.Endpoints.Helpers;
using CustomCADs.Auth.Infrastructure;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Infrastructure.Email;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        => services
            .AddAuthInfrastructure(config)
            .AddAuthApplication(config)
            .AddAppIdentity()
            .AddEndpoints();

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        => app
            .UseGlobalExceptionHandler()
            .UseEndpoints();

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

    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddEndpointsApiExplorer();

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
