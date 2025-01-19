using CustomCADs.Orders.Domain.CompletedOrders;
using CustomCADs.Orders.Domain.CompletedOrders.Enums;
using CustomCADs.Orders.Domain.CompletedOrders.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Persistence.CompletedOrders.Reads;

public static class Utilities
{
    public static IQueryable<CompletedOrder> WithFilter(this IQueryable<CompletedOrder> query, bool? delivery = null, AccountId? buyerId = null, AccountId? designerId = null)
    {
        if (delivery is not null)
        {
            query = query.Where(c => c.Delivery);
        }
        if (buyerId is not null)
        {
            query = query.Where(c => c.BuyerId == buyerId);
        }
        if (designerId is not null)
        {
            query = query.Where(c => c.DesignerId == designerId);
        }

        return query;
    }

    public static IQueryable<CompletedOrder> WithSearch(this IQueryable<CompletedOrder> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<CompletedOrder> WithSorting(this IQueryable<CompletedOrder> query, CompletedOrderSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: CompletedOrderSortingType.OrderDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.OrderDate),
            { Type: CompletedOrderSortingType.OrderDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.OrderDate),
            { Type: CompletedOrderSortingType.PurchaseDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.PurchaseDate),
            { Type: CompletedOrderSortingType.PurchaseDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.PurchaseDate),
            { Type: CompletedOrderSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: CompletedOrderSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: CompletedOrderSortingType.Delivery, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Delivery),
            { Type: CompletedOrderSortingType.Delivery, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Delivery),
            _ => query,
        };
    }

    public static IQueryable<CompletedOrder> WithPagination(this IQueryable<CompletedOrder> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
