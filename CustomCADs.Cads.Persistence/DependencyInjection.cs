using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Persistence;
using CustomCADs.Cads.Persistence.Cads.Reads;
using CustomCADs.Cads.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddOrdersContext(config)
            .AddReads()
            .AddWrites()
            .AddUOW();

    public static IServiceCollection AddOrdersContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("OrdersConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'OrdersConnection'.");
        services.AddDbContext<CadsContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<ICadReads, CadReads>();

        return services;
    }

    public static IServiceCollection AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    public static IServiceCollection AddUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
