﻿using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Delivery.Persistence;
using CustomCADs.Delivery.Persistence.Common;
using CustomCADs.Delivery.Persistence.Shipments.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateDeliveryContextAsync(this IServiceProvider provider)
    {
        DeliveryContext context = provider.GetRequiredService<DeliveryContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddDeliveryPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<DeliveryContext>(options =>
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Delivery")
            )
        );

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<IShipmentReads, ShipmentReads>();

        return services;
    }

    public static IServiceCollection AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
