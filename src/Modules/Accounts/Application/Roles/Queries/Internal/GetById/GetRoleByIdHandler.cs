using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;

public sealed class GetRoleByIdHandler(IRoleReads reads, ICacheService cache)
	: IQueryHandler<GetRoleByIdQuery, RoleReadDto>
{
	public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
	{
		Role? role = await cache.GetRoleAsync(req.Id).ConfigureAwait(false);

		if (role is null)
		{
			role = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Role>.ById(req.Id);

			await cache.SetRoleAsync(role.Id, role).ConfigureAwait(false);
			await cache.SetRoleAsync(role.Name, role).ConfigureAwait(false);
		}

		return role.ToDto();
	}
}
