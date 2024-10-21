#pragma warning disable IDE0130
using CustomCADs.Catalog.Application;
using Mapster;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddWolverine(cfg => cfg.ApplicationAssembly = CatalogApplicationReference.Assembly);
    }

#pragma warning disable IDE0060
    public static void AddApplicationMappers(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(CatalogApplicationReference.Assembly);
    }
}
