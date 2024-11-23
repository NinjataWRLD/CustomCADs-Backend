using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Queries.IsCreator;

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
