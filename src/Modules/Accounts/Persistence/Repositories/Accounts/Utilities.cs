using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Accounts.Domain.Accounts.ValueObjects;
using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Accounts.Persistence.Repositories.Accounts;

public static class Utilities
{
	public static IQueryable<Account> WithFilter(this IQueryable<Account> query, AccountId[]? ids, string? role = null)
	{
		if (!string.IsNullOrEmpty(role))
		{
			query = query.Where(u => u.RoleName == role);
		}
		if (ids is not null)
		{
			query = query.Where(u => ids.Contains(u.Id));
		}

		return query;
	}

	public static IQueryable<Account> WithSearch(this IQueryable<Account> query, string? username = null, string? email = null, string? firstName = null, string? lastName = null)
	{
		if (!string.IsNullOrWhiteSpace(username))
		{
			query = query.Where(u => u.Username.ToLower().Contains(username.ToLower()));
		}
		if (!string.IsNullOrWhiteSpace(email))
		{
			query = query.Where(u => u.Email.Contains(email));
		}
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			query = query.Where(u => u.FirstName != null && u.FirstName.ToLower().Contains(firstName.ToLower()));
		}
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			query = query.Where(u => u.LastName != null && u.LastName.ToLower().Contains(lastName.ToLower()));
		}

		return query;
	}

	public static IQueryable<Account> WithSorting(this IQueryable<Account> query, AccountSorting? sorting = null)
		=> sorting switch
		{
			{ Type: AccountSortingType.Username, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.Username),
			{ Type: AccountSortingType.Username, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.Username),
			{ Type: AccountSortingType.Email, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.Email),
			{ Type: AccountSortingType.Email, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.Email),
			{ Type: AccountSortingType.Role, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.RoleName),
			{ Type: AccountSortingType.Role, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.RoleName),
			_ => query,
		};

	public static IQueryable<Account> WithPagination(this IQueryable<Account> query, int page = 1, int limit = 20)
		=> query.Skip((page - 1) * limit).Take(limit);

	public static async Task<ProductId[]> GetViewedProductsByAccountIdAsync(this DbSet<ViewedProduct> set, AccountId id, CancellationToken ct = default)
		=> await set
			.Where(x => x.AccountId == id)
			.Select(x => x.ProductId)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public static async Task<ProductId[]> GetViewedProductsByAccountUsernrameAsync(this DbSet<ViewedProduct> set, string username, CancellationToken ct = default)
		=> await set
			.Include(x => x.Account)
			.Where(x => x.Account.Username == username)
			.Select(x => x.ProductId)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);
}
