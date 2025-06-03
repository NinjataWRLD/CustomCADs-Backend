using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetByName;

public sealed class GetRoleByNameHandler(IRoleReads reads, ICacheService cache)
	: IQueryHandler<GetRoleByNameQuery, RoleReadDto>
{
	public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
	{
		Role? role = await cache.GetRoleAsync(req.Name).ConfigureAwait(false);

		if (role is null)
		{
			role = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Role>.ByProp(nameof(req.Name), req.Name);

			await cache.SetRoleAsync(role.Id, role).ConfigureAwait(false);
			await cache.SetRoleAsync(role.Name, role).ConfigureAwait(false);
		}

		return role.ToDto();
	}
}
