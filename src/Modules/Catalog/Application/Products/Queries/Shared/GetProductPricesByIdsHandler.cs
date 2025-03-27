using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared;

public class GetProductPricesByIdsHandler(IProductReads reads)
    : IQueryHandler<GetProductPricesByIdsQuery, Dictionary<ProductId, decimal>>
{
    public async Task<Dictionary<ProductId, decimal>> Handle(GetProductPricesByIdsQuery req, CancellationToken ct)
    {
        ProductQuery query = new(
            Pagination: new(1, req.Ids.Length),
            Ids: req.Ids
        );
        Result<Product> result = await reads.AllAsync(query, track: false, ct).ConfigureAwait(false);

        return result.Items.ToDictionary(i => i.Id, i => i.Price);
    }
}
