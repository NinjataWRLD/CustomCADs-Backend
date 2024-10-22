using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Account.Persistence;
using CustomCADs.Account.Persistence.Repositories;
using CustomCADs.Account.Persistence.Repositories.Reads;
using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCatalogContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AccountConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AccountConnection'.");
        services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));
    }

    public static void AddCatalogReads(this IServiceCollection services)
    {
        services.AddScoped<IRoleReads, RoleReads>();
        services.AddScoped<IUserReads, UserReads>();
    }

    public static void AddCatalogWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));
    }

    public static void AddCatalogUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
