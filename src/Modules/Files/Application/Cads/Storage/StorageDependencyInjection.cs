using CustomCADs.Files.Application.Cads.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class StorageDependencyInjection
{
	public static IServiceCollection AddCadStorageService(this IServiceCollection services)
		=> services.AddScoped<ICadStorageService, CadStorageService>();
}
