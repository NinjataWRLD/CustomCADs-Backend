using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Files.Persistence;
using CustomCADs.Files.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using CadReads = CustomCADs.Files.Persistence.Repositories.Cads.Reads;
using ImageReads = CustomCADs.Files.Persistence.Repositories.Images.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateFilesContextAsync(this IServiceProvider provider)
	{
		FilesContext context = provider.GetRequiredService<FilesContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddFilesPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddContext(config)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
	{
		string connectionString = config.GetConnectionString("ApplicationConnection")
			?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

		services.AddDbContext<FilesContext>(options =>
			options.UseNpgsql(connectionString, opt =>
				opt.MigrationsHistoryTable("__EFMigrationsHistory", "Files")
			)
		);

		return services;
	}

	public static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<ICadReads, CadReads>();
		services.AddScoped<IImageReads, ImageReads>();

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
