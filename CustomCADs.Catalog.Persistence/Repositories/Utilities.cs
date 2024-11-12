using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Persistence.Repositories;

public static class Utilities
{
    public static IQueryable<Product> WithFilter(this IQueryable<Product> query, UserId? creatorId = null, string? status = null)
    {
        if (creatorId is not null)
        {
            query = query.Where(c => c.CreatorId == creatorId);
        }
        if (status is not null && Enum.TryParse(status, ignoreCase: true, out ProductStatus cadStatus))
        {
            query = query.Where(c => c.Status == cadStatus);
        }

        return query;
    }

    public static IQueryable<Product> WithSearch(this IQueryable<Product> query, string? category = null, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(c => c.Category.Name == category);
        }
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
            { Type: ProductSortingType.Category, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Category.Name),
            { Type: ProductSortingType.Category, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Category.Name),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.Status),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.Status),
            { Type: ProductSortingType.CostAmount, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Price.Amount),
            { Type: ProductSortingType.CostAmount, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Price.Amount),
            { Type: ProductSortingType.CostCurrency, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Price.Currency),
            { Type: ProductSortingType.CostCurrency, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Price.Currency),
            _ => query,
        };
    }

    public static IQueryable<Product> WithPagination(this IQueryable<Product> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}
