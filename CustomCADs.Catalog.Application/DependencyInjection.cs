#pragma warning disable IDE0130
using CustomCADs.Catalog.Application;
using Mapster;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CatalogApplicationAssemblyReference>());
    }

#pragma warning disable IDE0060
    public static void AddApplicationMappers(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(CatalogApplicationAssemblyReference.Assembly);
    }
}
