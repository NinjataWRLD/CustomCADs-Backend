using CustomCADs.Catalog.Application.Categories.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddCategoryCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<CategoryId, Category>, CategoryCachingService>();
}
