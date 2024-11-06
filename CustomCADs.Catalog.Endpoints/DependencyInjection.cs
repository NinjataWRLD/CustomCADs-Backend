#pragma warning disable IDE0130
using CustomCADs.Catalog.Endpoints.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration config)
        => services.AddCatalogPersistence(config);

    public static IApplicationBuilder UseCatalog(this IApplicationBuilder app)
        => app.UseGlobalExceptionHandler();

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
