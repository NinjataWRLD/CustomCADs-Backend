using CustomCADs.Account.Domain.Users.Entities;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Account.Persistence.Repositories.Reads;

public class UserReads(AccountContext context) : IUserReads
{
    public async Task<UserResult> AllAsync(UserQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<User> queryable = context.Users
            .WithTracking(track)
            .WithFilter(query.Ids, query.Role)
            .WithSearch(query.Username, query.Email, query.FirstName, query.LastName)
            .WithSorting(query.Sorting);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        User[] users = await queryable
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, users);
    }

    public async Task<User?> SingleByIdAsync(UserId id, bool track = true, CancellationToken ct = default)
        => await context.Users
            .WithTracking(track)
            .FirstOrDefaultAsync(u => u.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<User?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default)
        => await context.Users
            .WithTracking(track)
            .FirstOrDefaultAsync(u => u.Username == username, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(UserId id, CancellationToken ct = default)
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
