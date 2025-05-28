using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Accounts.Domain.Accounts.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;

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
			query = query.Where(u => u.Username.Contains(username, StringComparison.InvariantCultureIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(email))
		{
			query = query.Where(u => u.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			query = query.Where(u => u.FirstName != null && u.FirstName.Contains(firstName, StringComparison.InvariantCultureIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			query = query.Where(u => u.LastName != null && u.LastName.Contains(lastName, StringComparison.InvariantCultureIgnoreCase));
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
}
