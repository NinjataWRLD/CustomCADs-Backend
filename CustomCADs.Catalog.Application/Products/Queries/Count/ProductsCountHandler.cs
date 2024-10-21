using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public class ProductsCountHandler(IProductReads reads)
{
    public async Task<int> Handle(ProductsCountQuery req, CancellationToken ct)
    {
        int count = await reads.CountAsync(req.CreatorId, req.Status, ct: ct).ConfigureAwait(false);

        return count;
    }
}
