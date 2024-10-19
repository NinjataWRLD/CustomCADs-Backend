using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.IsCreator;

public class IsProductCreatorHandler(IProductReads reads) : IRequestHandler<IsProductCreatorQuery, bool>
{
    public async Task<bool> Handle(IsProductCreatorQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        var result = product.CreatorId == req.CreatorId;
        return result;
    }
}
