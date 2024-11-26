using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common;
using CustomCADs.Gallery.Persistence;
using CustomCADs.Gallery.Persistence.Carts.Reads;
using CustomCADs.Gallery.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGalleryPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddGalleryContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    private static IServiceCollection AddGalleryContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("GalleryConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'GalleryConnection'.");
        
        services.AddDbContext<GalleryContext>(options => 
            options.UseSqlServer(connectionString, opt
                => opt.MigrationsHistoryTable("__EFMigrationsHistory", "Gallery")
            )
        );

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<ICartReads, CartReads>();

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
