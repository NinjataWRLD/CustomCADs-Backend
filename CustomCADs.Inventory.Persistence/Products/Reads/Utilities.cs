using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Persistence.Products.Reads;

public static class Utilities
{
    public static IQueryable<Product> WithFilter(this IQueryable<Product> query, ProductId[]? ids, AccountId? creatorId = null, CategoryId? categoryId = null, ProductStatus? productStatus = null)
    {
        if (ids is not null)
        {
            query = query.Where(c => ids.Contains(c.Id));
        }
        if (creatorId is not null)
        {
            query = query.Where(c => c.CreatorId == creatorId);
        }
        if (categoryId is not null)
        {
            query = query.Where(c => c.CategoryId == categoryId);
        }
        if (productStatus is not null)
        {
            query = query.Where(c => c.Status == productStatus);
        }

        return query;
    }

    public static IQueryable<Product> WithSearch(this IQueryable<Product> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<Product> WithSorting(this IQueryable<Product> query, ProductSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: ProductSortingType.UploadDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.UploadDate),
            { Type: ProductSortingType.UploadDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.UploadDate),
            { Type: ProductSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: ProductSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.Status),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.Status),
            { Type: ProductSortingType.Cost, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Price),
            { Type: ProductSortingType.Cost, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Price),
            { Type: ProductSortingType.Purchases, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Counts.Purchases),
            { Type: ProductSortingType.Purchases, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Counts.Purchases),
            { Type: ProductSortingType.Likes, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Counts.Likes),
            { Type: ProductSortingType.Likes, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Counts.Likes),
            { Type: ProductSortingType.Views, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Counts.Views),
            { Type: ProductSortingType.Views, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Counts.Views),
            _ => query,
        };
    }

    public static IQueryable<Product> WithPagination(this IQueryable<Product> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
