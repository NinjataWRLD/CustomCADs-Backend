using CustomCADs.Catalog.Application.Tags.Caching;
using CustomCADs.Catalog.Domain.Tags;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddTagCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<TagId, Tag>, TagCachingService>();
}
