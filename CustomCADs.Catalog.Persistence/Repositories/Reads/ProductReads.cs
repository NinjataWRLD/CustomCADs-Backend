using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Persistence.Repositories.Reads;

public class ProductReads(CatalogContext context) : IProductReads
{
    public async Task<IEnumerable<Product>> AllAsync(bool track = true, CancellationToken ct = default)
        => await context.Products
            .WithTracking(track)
            .Include(p => p.Category)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

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
