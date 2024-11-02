using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Account.Persistence;
using CustomCADs.Account.Persistence.Repositories;
using CustomCADs.Account.Persistence.Repositories.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddAccountContext(config)
            .AddAccountReads()
            .AddAccountWrites()
            .AddAccountUOW();

    private static IServiceCollection AddAccountContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AccountConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AccountConnection'.");
        services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection AddAccountReads(this IServiceCollection services)
    {
        services.AddScoped<IRoleReads, RoleReads>();
        services.AddScoped<IUserReads, UserReads>();

        return services;
    }

    private static IServiceCollection AddAccountWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddAccountUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
