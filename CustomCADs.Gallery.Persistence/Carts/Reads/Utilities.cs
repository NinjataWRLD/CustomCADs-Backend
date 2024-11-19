using CustomCADs.Gallery .Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Gallery.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Gallery.Persistence.Carts.Reads;

public static class Utilities
{
    public static IQueryable<Cart> WithFilter(this IQueryable<Cart> query, UserId? buyerId = null)
    {
        if (buyerId is not null)
        {
            query = query.Where(c => c.BuyerId == buyerId);
        }

        return query;
    }

    public static IQueryable<Cart> WithSorting(this IQueryable<Cart> query, CartSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: CartSortingType.PurchaseDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.PurchaseDate),
            { Type: CartSortingType.PurchaseDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.PurchaseDate),
            { Type: CartSortingType.Total, Direction: SortingDirection.Ascending } => query.OrderByDescending(c => c.Total),
            { Type: CartSortingType.Total, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Total),
            _ => query,
        };
    }

    public static IQueryable<Cart> WithPagination(this IQueryable<Cart> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
