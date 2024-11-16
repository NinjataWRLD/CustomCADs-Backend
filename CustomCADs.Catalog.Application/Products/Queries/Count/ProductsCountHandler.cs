using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public class ProductsCountHandler(IProductReads reads)
    : IQueryHandler<ProductsCountQuery, int>
{
    public async Task<int> Handle(ProductsCountQuery req, CancellationToken ct)
    {
        return await reads.CountByStatusAsync(req.CreatorId, req.Status, ct: ct).ConfigureAwait(false);
    }
}
