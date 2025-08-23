namespace CustomCADs.Accounts.Application.Roles.Caching;

public class RolesCachingService(ICacheService service) : BaseCachingService<RoleId, Role>
{
	private const string BaseKey = "roles";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(RoleId id) => $"{BaseKey}:{id}";

	public override async Task<ICollection<Role>> GetOrCreateAsync(Func<Task<ICollection<Role>>> factory)
		=> await service.GetOrCreateAsync(
				key: GetKey(),
				factory: factory,
				expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
			).ConfigureAwait(false)
			?? throw CustomCachingException<Role>.ByKey(GetKey());

	public override async Task<Role> GetOrCreateAsync(RoleId id, Func<Task<Role>> factory)
		=> await service.GetOrCreateAsync(
				key: GetKey(id),
				factory: factory,
				expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
			).ConfigureAwait(false)
			?? throw CustomCachingException<Role>.ByKey(GetKey(id));

	public override async Task UpdateAsync(RoleId id, Role item)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), item).ConfigureAwait(false);
	}

	public override async Task ClearAsync(RoleId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
