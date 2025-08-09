#pragma warning disable IDE0130
using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Identity.Infrastructure.Identity;
using CustomCADs.Identity.Infrastructure.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection;

using AppRoleWrites = CustomCADs.Identity.Infrastructure.Identity.Repositories.Roles.Writes;
using AppUserReads = CustomCADs.Identity.Infrastructure.Identity.Repositories.Users.Reads;
using AppUserWrites = CustomCADs.Identity.Infrastructure.Identity.Repositories.Users.Writes;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateIdentityContextAsync(this IServiceProvider provider)
	{
		IdentityContext context = provider.GetRequiredService<IdentityContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddIdentityPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddContext(config)
			.AddManagers();


	private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
	{
		string connectionString = config.GetConnectionString("ApplicationConnection")
			?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

		services.AddDbContext<IdentityContext>(options =>
			options.UseNpgsql(
				dataSource: new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson().Build(),
				npgsqlOptionsAction: opt =>
				opt.MigrationsHistoryTable("__EFMigrationsHistory", "Identity")
			)
		);

		return services;
	}

	private static IServiceCollection AddManagers(this IServiceCollection services)
	{
		services.AddScoped<IUserReads, AppUserReads>();
		services.AddScoped<IUserWrites, AppUserWrites>();
		services.AddScoped<IRoleWrites, AppRoleWrites>();

		return services;
	}

	public static void AddTokensService(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, IdentityTokenService>();
	}
}
