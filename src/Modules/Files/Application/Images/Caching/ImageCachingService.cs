using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Application.Images.Caching;

public class ImageCachingService(ICacheService service) : BaseCachingService<ImageId, Image>
{
	private const string BaseKey = "images";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(ImageId id) => $"{BaseKey}:{id}";

	public async override Task<ICollection<Image>> GetOrCreateAsync(Func<Task<ICollection<Image>>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Image>.ByKey(GetKey());

	public async override Task<Image> GetOrCreateAsync(ImageId id, Func<Task<Image>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(id),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Image>.ByKey(GetKey(id));

	public async override Task UpdateAsync(ImageId id, Image shipment)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), shipment).ConfigureAwait(false);
	}

	public async override Task ClearAsync(ImageId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
