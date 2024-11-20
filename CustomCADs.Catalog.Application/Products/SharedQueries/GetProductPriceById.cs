using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Queries.Products;

namespace CustomCADs.Catalog.Application.Products.SharedQueries;

public class GetProductPriceByIdHandler(IProductReads reads)
    : IQueryHandler<GetProductPriceByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetProductPriceByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        return product.Price.Amount;
    }
}
