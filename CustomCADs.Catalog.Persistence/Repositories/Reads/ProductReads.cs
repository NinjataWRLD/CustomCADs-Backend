using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Persistence.Repositories.Reads;

public class ProductReads(CatalogContext context) : IProductReads
{
    public async Task<ProductResult> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Product> queryable = context.Products
                .WithTracking(track)
                .Include(p => p.Category)
                .WithFilter(query.CreatorId, query.Status)
                .WithSearch(query.Category, query.Name)
                .WithSorting(query.Sorting);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Product[] products = await queryable
                .WithPagination(query.Page, query.Limit)
                .ToArrayAsync(ct)
                .ConfigureAwait(false);

        return new()
        {
            Count = count,
            Products = products,
        };
    }

    public async Task<Product?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default)
        => await context.Products
            .WithTracking(track)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Products
            .WithTracking(false)
            .AnyAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(Guid creatorId, ProductStatus status, CancellationToken ct = default)
        => await context.Products
            .WithTracking(false)
            .Where(p => p.CreatorId == creatorId)
            .CountAsync(p => p.Status == status, ct)
            .ConfigureAwait(false);
}
