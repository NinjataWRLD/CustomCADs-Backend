using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Catalog.Application.Products.SharedQueryHandler;

public sealed class GetProductPriceByIdHandler(IProductReads reads)
    : IQueryHandler<GetProductPriceByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetProductPriceByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        return product.Price;
    }
}
