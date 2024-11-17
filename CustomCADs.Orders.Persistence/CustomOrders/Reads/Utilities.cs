using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.ValueObjects;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Persistence.CustomOrders.Reads;

public static class Utilities
{
    public static IQueryable<CustomOrder> WithFilter(this IQueryable<CustomOrder> query, DeliveryType? deliveryType = null, CustomOrderStatus? orderStatus = null, UserId? buyerId = null, UserId? designerId = null)
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

    public static IQueryable<CustomOrder> WithSearch(this IQueryable<CustomOrder> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<CustomOrder> WithSorting(this IQueryable<CustomOrder> query, CustomOrderSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: CustomOrderSortingType.OrderDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.OrderDate),
            { Type: CustomOrderSortingType.OrderDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.OrderDate),
            { Type: CustomOrderSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: CustomOrderSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: CustomOrderSortingType.OrderStatus, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.OrderStatus),
            { Type: CustomOrderSortingType.OrderStatus, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.OrderStatus),
            { Type: CustomOrderSortingType.DeliveryType, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.DeliveryType),
            { Type: CustomOrderSortingType.DeliveryType, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.DeliveryType),
            _ => query,
        };
    }

    public static IQueryable<CustomOrder> WithPagination(this IQueryable<CustomOrder> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
