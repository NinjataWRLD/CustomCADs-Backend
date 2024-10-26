﻿using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Account.Persistence.Repositories.Reads;

public class RoleReads(AccountContext context) : IRoleReads
{
    public async Task<IEnumerable<Role>> AllAsync(bool track = true, CancellationToken ct = default)
        => await context.Roles
            .WithTracking(track)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<Role?> SingleByIdAsync(int id, bool track = true, CancellationToken ct = default)
        => await context.Roles
            .WithTracking(track)
            .SingleOrDefaultAsync(r => r.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<Role?> SingleByNameAsync(string name, bool track = true, CancellationToken ct = default)
        => await context.Roles
            .WithTracking(track)
            .SingleOrDefaultAsync(r => r.Name == name, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken ct = default)
        => await context.Roles
            .WithTracking(false)
            .AnyAsync(r => r.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => await context.Roles
            .WithTracking(false)
            .AnyAsync(r => r.Name == name, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await context.Roles
            .WithTracking(false)
            .CountAsync(ct)
            .ConfigureAwait(false);
}