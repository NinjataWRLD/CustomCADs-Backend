﻿using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public class GetRoleByIdHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetRoleByIdQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
    {
        Role role =
            await cache.GetAsync<Role>($"roles/{req.Id}").ConfigureAwait(false)
            ?? await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ById(req.Id);

        RoleReadDto response = new(role.Id, role.Name, role.Description);
        return response;
    }
}
