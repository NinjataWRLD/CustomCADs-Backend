using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Inventory.Persistence.Products.Reads;

public class ProductReads(InventoryContext context) : IProductReads
{
    public async Task<Result<Product>> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Product> queryable = context.Products
                .WithTracking(track)
                .WithFilter(query.CreatorId, query.CategoryId, query.Status)
                .WithSearch(query.Name)
                .WithSorting(query.Sorting ?? new());

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Product[] products = await queryable
                .WithPagination(query.Page, query.Limit)
                .ToArrayAsync(ct)
                .ConfigureAwait(false);

        return new(count, products);
    }

    public async Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default)
        => await context.Products
            .WithTracking(track)
            .FirstOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default)
        => await context.Products
            .WithTracking(false)
            .AnyAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<Dictionary<ProductStatus, int>> CountByStatusAsync(AccountId creatorId, CancellationToken ct = default)
        => await context.Products
            .WithTracking(false)
            .Where(p => p.CreatorId == creatorId)
            .GroupBy(p => p.Status)
            .ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
            .ConfigureAwait(false);
}
