using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Inventory.Persistence.Products.Reads;

public class ProductReads(InventoryContext context) : IProductReads
{
    public async Task<ProductResult> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Product> queryable = context.Products
                .WithTracking(track)
                .WithFilter(query.CreatorId, query.Status)
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

    public async Task<int> CountByStatusAsync(UserId creatorId, ProductStatus status, CancellationToken ct = default)
        => await context.Products
            .WithTracking(false)
            .Where(p => p.CreatorId == creatorId)
            .CountAsync(p => p.Status == status, ct)
            .ConfigureAwait(false);
}
