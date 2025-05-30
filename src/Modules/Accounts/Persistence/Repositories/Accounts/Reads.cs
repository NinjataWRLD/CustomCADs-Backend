using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Accounts.Persistence.Repositories.Accounts;

public sealed class Reads(AccountsContext context) : IAccountReads
{
	public async Task<Result<Account>> AllAsync(AccountQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Account> queryable = context.Accounts
			.WithTracking(track)
			.WithFilter(query.Ids, query.Role)
			.WithSearch(query.Username, query.Email, query.FirstName, query.LastName)
			.WithSorting(query.Sorting);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Account[] accounts = await queryable
			.WithPagination(query.Pagination.Page, query.Pagination.Limit)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, accounts);
	}

	public async Task<Account?> SingleByIdAsync(AccountId id, bool track = true, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(track)
			.FirstOrDefaultAsync(u => u.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Account?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(track)
			.FirstOrDefaultAsync(u => u.Username == username, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(AccountId id, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.AnyAsync(u => u.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.AnyAsync(u => u.Username == username, ct)
			.ConfigureAwait(false);

	public async Task<int> CountAsync(CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.CountAsync(ct)
			.ConfigureAwait(false);
}
