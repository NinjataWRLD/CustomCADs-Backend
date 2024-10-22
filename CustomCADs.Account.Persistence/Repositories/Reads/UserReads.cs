﻿using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Account.Persistence.Repositories.Reads;

public class UserReads(AccountContext context) : IUserReads
{
    public async Task<IEnumerable<User>> AllAsync(bool track = true, CancellationToken ct = default)
        => await context.Users
            .WithTracking(track)
            // .WithFilter()
            // .WithSearch()
            // .WithSorting()
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<User?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default)
        => await context.Users
            .WithTracking(track)
            .FirstOrDefaultAsync(u => u.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<User?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default)
        => await context.Users
            .WithTracking(track)
            .FirstOrDefaultAsync(u => u.Username == username, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Users
            .WithTracking(false)
            .AnyAsync(u => u.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default)
        => await context.Users
            .WithTracking(false)
            .AnyAsync(u => u.Username == username, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await context.Users
            .WithTracking(false)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
