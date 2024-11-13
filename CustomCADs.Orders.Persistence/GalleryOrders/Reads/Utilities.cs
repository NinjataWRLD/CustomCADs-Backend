﻿using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using CustomCADs.Orders.Domain.GalleryOrders.Enums;
using CustomCADs.Orders.Domain.GalleryOrders.ValueObjects;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Persistence.GalleryOrders.Reads;

public static class Utilities
{
    public static IQueryable<GalleryOrder> WithFilter(this IQueryable<GalleryOrder> query, string? type = null, UserId? buyerId = null)
    {
        if (type is not null && Enum.TryParse(type, ignoreCase: true, out DeliveryType deliveryType))
        {
            query = query.Where(c => c.DeliveryType == deliveryType);
        }
        if (buyerId is not null)
        {
            query = query.Where(c => c.BuyerId == buyerId);
        }

        return query;
    }

    public static IQueryable<GalleryOrder> WithSorting(this IQueryable<GalleryOrder> query, GalleryOrderSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: GalleryOrderSortingType.PurchaseDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.PurchaseDate),
            { Type: GalleryOrderSortingType.PurchaseDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.PurchaseDate),
            { Type: GalleryOrderSortingType.Total, Direction: SortingDirection.Ascending } => query.OrderByDescending(c => c.Total),
            { Type: GalleryOrderSortingType.Total, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Total),
            { Type: GalleryOrderSortingType.DeliveryType, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.DeliveryType),
            { Type: GalleryOrderSortingType.DeliveryType, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.DeliveryType),
            _ => query,
        };
    }

    public static IQueryable<GalleryOrder> WithPagination(this IQueryable<GalleryOrder> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}