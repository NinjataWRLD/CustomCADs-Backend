#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class CachingDependencyInjection
{
	public static IServiceCollection AddMaterialCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<MaterialId, Material>>();

}
