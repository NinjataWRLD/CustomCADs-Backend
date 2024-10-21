using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using Mapster;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads)
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        var response = product.Adapt<GetProductByIdDto>();
        return response;
    }
}
