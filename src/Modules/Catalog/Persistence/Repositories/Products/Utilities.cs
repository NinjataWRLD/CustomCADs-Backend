using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Catalog.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Persistence.Repositories.Products;

public static class Utilities
{
    public static IQueryable<Product> WithFilter(this IQueryable<Product> query, ProductId[]? ids, AccountId? creatorId = null, AccountId? designerId = null, CategoryId? categoryId = null, ProductStatus? status = null)
    {
        if (ids is not null)
        {
            query = query.Where(c => ids.Contains(c.Id));
        }
        if (creatorId is not null)
        {
            query = query.Where(c => c.CreatorId == creatorId);
        }
        if (designerId is not null)
        {
            query = query.Where(c => c.DesignerId == designerId);
        }
        if (categoryId is not null)
        {
            query = query.Where(c => c.CategoryId == categoryId);
        }
        if (status is not null)
        {
            query = query.Where(c => c.Status == status);
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
            { Type: ProductSortingType.UploadedAt, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.UploadedAt),
            { Type: ProductSortingType.UploadedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.UploadedAt),
            { Type: ProductSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: ProductSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.Status),
            { Type: ProductSortingType.Status, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.Status),
            { Type: ProductSortingType.Cost, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Price),
            { Type: ProductSortingType.Cost, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Price),
            { Type: ProductSortingType.Purchases, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Counts.Purchases),
            { Type: ProductSortingType.Purchases, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Counts.Purchases),
            { Type: ProductSortingType.Views, Direction: SortingDirection.Ascending } => query.OrderBy(m => m.Counts.Views),
            { Type: ProductSortingType.Views, Direction: SortingDirection.Descending } => query.OrderByDescending(m => m.Counts.Views),
            _ => query,
        };
    }

    public static IQueryable<Product> WithPagination(this IQueryable<Product> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }

    public static async Task<ProductId[]?> GetProductIdsByTagIdsOrDefaultAsync(this DbSet<ProductTag> set, TagId[]? tagIds, CancellationToken ct = default)
        => tagIds is not null
            ? await set
                .Where(x => tagIds.Contains(x.TagId))
                .GroupBy(x => x.ProductId)
                .Where(x => x.Count() == tagIds.Length)
                .Select(x => x.Key)
                .ToArrayAsync(ct)
                .ConfigureAwait(false)
            : null;
}
