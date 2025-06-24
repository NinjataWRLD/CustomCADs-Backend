using CustomCADs.Accounts.Domain.Repositories.Reads;

namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;

public sealed class GetRoleByIdHandler(IRoleReads reads, BaseCachingService<RoleId, Role> cache)
	: IQueryHandler<GetRoleByIdQuery, RoleReadDto>
{
	public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
	{
		Role role = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Role>.ById(req.Id)
		).ConfigureAwait(false);

		return role.ToDto();
	}
}
