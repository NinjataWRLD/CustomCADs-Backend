﻿using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;

public sealed class GetAllRolesHandler(IRoleReads reads, ICacheService cache)
	: IQueryHandler<GetAllRolesQuery, IEnumerable<RoleReadDto>>
{
	public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
	{
		IEnumerable<Role>? roles = await cache.GetRolesArrayAsync().ConfigureAwait(false);

		if (roles is null)
		{
			roles = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);
			await cache.SetRolesArrayAsync(roles).ConfigureAwait(false);
		}

		return roles.Select(r => r.ToDto());
	}
}
