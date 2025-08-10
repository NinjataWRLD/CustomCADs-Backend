#pragma warning disable IDE0130
using CustomCADs.Catalog.Application.Categories.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static class CachingDependencyInjection
{
	public static IServiceCollection AddCategoryCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<CategoryId, Category>, CategoryCachingService>();

}
