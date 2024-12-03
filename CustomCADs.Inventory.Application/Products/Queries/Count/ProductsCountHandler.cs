using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Queries.Count;

public sealed class ProductsCountHandler(IProductReads reads)
    : IQueryHandler<ProductsCountQuery, ProductsCountDto>
{
    public async Task<ProductsCountDto> Handle(ProductsCountQuery req, CancellationToken ct)
    {
        Dictionary<ProductStatus, int> counts = await reads
            .CountByStatusAsync(req.CreatorId, ct: ct).ConfigureAwait(false);

        return new(
            Unchecked: counts.TryGetValue(ProductStatus.Unchecked, out int uncheckedCount)
                ? uncheckedCount : 0,

            Validated: counts.TryGetValue(ProductStatus.Validated, out int validatedCount)
                ? validatedCount : 0,

            Reported: counts.TryGetValue(ProductStatus.Reported, out int reportedCount)
                ? reportedCount : 0,

            Banned: counts.TryGetValue(ProductStatus.Removed, out int removedCount)
                ? removedCount : 0
        );
    }
}
