using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCategories(this IServiceCollection services, IConfiguration config)
        => services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddCategoriesPersistence(config);
}
