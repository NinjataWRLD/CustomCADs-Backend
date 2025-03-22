using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Persistence.Repositories.OngoingOrders;

public static class Utilities
{
    public static IQueryable<OngoingOrder> WithFilter(this IQueryable<OngoingOrder> query, bool? delivery = null, OngoingOrderStatus? orderStatus = null, AccountId? buyerId = null, AccountId? designerId = null)
    {
        if (delivery is not null)
        {
            query = query.Where(c => c.Delivery);
        }
        if (orderStatus is not null)
        {
            query = query.Where(c => c.OrderStatus == orderStatus);
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

    public static IQueryable<OngoingOrder> WithSearch(this IQueryable<OngoingOrder> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<OngoingOrder> WithSorting(this IQueryable<OngoingOrder> query, OngoingOrderSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: OngoingOrderSortingType.OrderDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.OrderDate),
            { Type: OngoingOrderSortingType.OrderDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.OrderDate),
            { Type: OngoingOrderSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: OngoingOrderSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: OngoingOrderSortingType.OrderStatus, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.OrderStatus),
            { Type: OngoingOrderSortingType.OrderStatus, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.OrderStatus),
            { Type: OngoingOrderSortingType.Delivery, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Delivery),
            { Type: OngoingOrderSortingType.Delivery, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Delivery),
            _ => query,
        };
    }

    public static IQueryable<OngoingOrder> WithPagination(this IQueryable<OngoingOrder> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
