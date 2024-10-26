#pragma warning disable IDE0130
using CustomCADs.Catalog.Application;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        => services
            .AddMediator();

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddWolverine(cfg => cfg.ApplicationAssembly = CatalogApplicationReference.Assembly);

        return services;
    }
}
