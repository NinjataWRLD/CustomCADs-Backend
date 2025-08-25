using CustomCADs.Files.Application.Images.Caching;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddImageCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<ImageId, Image>, ImageCachingService>();
}
