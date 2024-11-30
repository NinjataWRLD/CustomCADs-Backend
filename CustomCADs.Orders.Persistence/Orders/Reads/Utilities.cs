using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Persistence.Orders.Reads;

public static class Utilities
{
    public static IQueryable<Order> WithFilter(this IQueryable<Order> query, DeliveryType? deliveryType = null, OrderStatus? orderStatus = null, AccountId? buyerId = null, AccountId? designerId = null)
    {
        if (deliveryType is not null)
        {
            query = query.Where(c => c.DeliveryType == deliveryType);
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

    public static IQueryable<Order> WithSearch(this IQueryable<Order> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<Order> WithSorting(this IQueryable<Order> query, OrderSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: OrderSortingType.OrderDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.OrderDate),
            { Type: OrderSortingType.OrderDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.OrderDate),
            { Type: OrderSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: OrderSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: OrderSortingType.OrderStatus, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.OrderStatus),
            { Type: OrderSortingType.OrderStatus, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.OrderStatus),
            { Type: OrderSortingType.DeliveryType, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.DeliveryType),
            { Type: OrderSortingType.DeliveryType, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.DeliveryType),
            _ => query,
        };
    }

    public static IQueryable<Order> WithPagination(this IQueryable<Order> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
