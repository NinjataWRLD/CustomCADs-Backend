using CustomCADs.Accounts.Domain.Repositories.Reads;

namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;

public sealed class GetAllRolesHandler(IRoleReads reads, BaseCachingService<RoleId, Role> cache)
	: IQueryHandler<GetAllRolesQuery, IEnumerable<RoleReadDto>>
{
	public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
	{
		ICollection<Role> roles = await cache.GetOrCreateAsync(
			factory: async () => [.. await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)]
		).ConfigureAwait(false);

		return roles.Select(r => r.ToDto());
	}
}
