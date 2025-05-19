using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Categories.Persistence;
using CustomCADs.Categories.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using CategoryReads = CustomCADs.Categories.Persistence.Repositories.Categories.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateCategoriesContextAsync(this IServiceProvider provider)
	{
		CategoriesContext context = provider.GetRequiredService<CategoriesContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddCategoriesPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddContext(config)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
	{
		string connectionString = config.GetConnectionString("ApplicationConnection")
			?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

		services.AddDbContext<CategoriesContext>(options =>
			options.UseNpgsql(connectionString, opt =>
				opt.MigrationsHistoryTable("__EFMigrationsHistory", "Categories")
			)
		);

		return services;
	}

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<ICategoryReads, CategoryReads>();

		return services;
	}

	private static IServiceCollection AddWrites(this IServiceCollection services)
	{
		services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		return services;
	}

	private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
