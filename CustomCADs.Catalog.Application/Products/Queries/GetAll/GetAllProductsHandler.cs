using CustomCADs.Catalog.Domain.Products.Reads;
using Mapster;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads)
{
    public async Task<GetAllProductsDto> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery query = new(
            CreatorId: req.CreatorId,
            Status: req.Status,
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        ProductResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        var response = result.Adapt<GetAllProductsDto>();
        return response;
    }
}
