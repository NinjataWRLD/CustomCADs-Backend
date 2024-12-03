using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Inventory.Application.Products.SharedQueryHandler;

public sealed class GetProductPriceByIdHandler(IProductReads reads)
    : IQueryHandler<GetProductPriceByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetProductPriceByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        return product.Price;
    }
}
