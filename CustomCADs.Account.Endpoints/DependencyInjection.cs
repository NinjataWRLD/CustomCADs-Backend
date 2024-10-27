using CustomCADs.Account.Endpoints.Helpers;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services, IConfiguration config)
        => services
            .AddAccountPersistence(config)
            .AddAccountApplication()
            .AddEndpoints();

    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints();

        return services;
    }

    public static IApplicationBuilder UseAccount(this IApplicationBuilder app)
        => app
            .UseGlobalExceptionHandler()
            .UseEndpoints();

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
