using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Persistence.Repositories.PurchasedCarts;

public static class Utilities
{
	public static IQueryable<PurchasedCart> WithFilter(this IQueryable<PurchasedCart> query, AccountId? buyerId = null, PaymentStatus? paymentStatus = null)
	{
		if (buyerId is not null)
		{
			query = query.Where(c => c.BuyerId == buyerId);
		}
		if (paymentStatus is not null)
		{
			query = query.Where(c => c.PaymentStatus == paymentStatus);
		}

		return query;
	}

	public static IQueryable<PurchasedCart> WithSorting(this IQueryable<PurchasedCart> query, PurchasedCartSorting? sorting = null)
		=> sorting switch
		{
			{ Type: PurchasedCartSortingType.PurchasedAt, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.PurchasedAt),
			{ Type: PurchasedCartSortingType.PurchasedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.PurchasedAt),
			{ Type: PurchasedCartSortingType.Total, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Items.Sum(x => x.Quantity * x.Price)),
			{ Type: PurchasedCartSortingType.Total, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Items.Sum(x => x.Quantity * x.Price)),
			_ => query,
		};
}
