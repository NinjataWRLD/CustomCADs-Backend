using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Persistence.Repositories.PurchasedCarts;

public static class Utilities
{
	public static IQueryable<PurchasedCart> WithFilter(this IQueryable<PurchasedCart> query, AccountId? buyerId = null)
	{
		if (buyerId is not null)
		{
			query = query.Where(c => c.BuyerId == buyerId);
		}

		return query;
	}

	public static IQueryable<PurchasedCart> WithSorting(this IQueryable<PurchasedCart> query, PurchasedCartSorting? sorting = null)
	{
		return sorting switch
		{
			{ Type: PurchasedCartSortingType.PurchasedAt, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.PurchasedAt),
			{ Type: PurchasedCartSortingType.PurchasedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.PurchasedAt),
			{ Type: PurchasedCartSortingType.Total, Direction: SortingDirection.Ascending } => query.OrderByDescending(c => c.TotalCost),
			{ Type: PurchasedCartSortingType.Total, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.TotalCost),
			_ => query,
		};
	}

	public static IQueryable<PurchasedCart> WithPagination(this IQueryable<PurchasedCart> query, int page = 1, int limit = 20)
	{
		return query.Skip((page - 1) * limit).Take(limit);
	}
}
