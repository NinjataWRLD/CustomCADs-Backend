using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.IsCreator;

public class IsProductCreatorHandler(IProductReads reads)
    : IQueryHandler<IsProductCreatorQuery, bool>
{
    public async Task<bool> Handle(IsProductCreatorQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        return product.CreatorId == req.CreatorId;
    }
}
