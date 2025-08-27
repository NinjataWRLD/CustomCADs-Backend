using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Catalog.Application.Tags.Caching;

public class TagCachingService(ICacheService service) : BaseCachingService<TagId, Tag>
{
	private const string BaseKey = "tags";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(TagId id) => $"{BaseKey}:{id}";

	public override async Task<Result<Tag>> GetOrCreateAsync(Func<Task<Result<Tag>>> factory)
		=> await service.GetOrCreateAsync(
				key: GetKey(),
				factory: factory,
				expiration: new(
					Absolute: TimeSpan.FromDays(1),
					Sliding: null
				)
			).ConfigureAwait(false)
			?? throw CustomCachingException<Tag>.ByKey(GetKey());

	public override async Task<ICollection<Tag>> GetOrCreateAsync(Func<Task<ICollection<Tag>>> factory)
		=> await service.GetOrCreateAsync(
				key: GetKey(),
				factory: factory,
				expiration: new(
					Absolute: TimeSpan.FromDays(1),
					Sliding: null
				)
			).ConfigureAwait(false)
			?? throw CustomCachingException<Tag>.ByKey(GetKey());

	public async override Task<Tag> GetOrCreateAsync(TagId id, Func<Task<Tag>> factory)
		=> await service.GetOrCreateAsync(
				key: GetKey(id),
				factory: factory,
				expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
			).ConfigureAwait(false)
			?? throw CustomCachingException<Tag>.ByKey(GetKey(id));

	public async override Task UpdateAsync(TagId id, Tag tag)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), tag).ConfigureAwait(false);
	}

	public async override Task ClearAsync(TagId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
