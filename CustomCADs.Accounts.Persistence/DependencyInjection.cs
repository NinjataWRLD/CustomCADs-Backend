using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Accounts.Persistence;
using CustomCADs.Accounts.Persistence.Accounts.Reads;
using CustomCADs.Accounts.Persistence.Common;
using CustomCADs.Accounts.Persistence.Roles.Reads;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddAccountsContext(config)
            .AddAccountsReads()
            .AddAccountsWrites()
            .AddAccountsUnitOfWork();

    private static IServiceCollection AddAccountsContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AccountsConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AccountsConnection'.");

        services.AddDbContext<AccountsContext>(options =>
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Accounts")
            )
        );

        return services;
    }

    private static IServiceCollection AddAccountsReads(this IServiceCollection services)
    {
        services.AddScoped<IRoleReads, RoleReads>();
        services.AddScoped<IAccountReads, AccountReads>();

        return services;
    }

    private static IServiceCollection AddAccountsWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddAccountsUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
