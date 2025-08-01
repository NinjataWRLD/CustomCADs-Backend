using CustomCADs.Files.Application.Images.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class StorageDependencyInjection
{
	public static IServiceCollection AddImageStorageService(this IServiceCollection services)
		=> services.AddScoped<IImageStorageService, ImageStorageService>();
}
