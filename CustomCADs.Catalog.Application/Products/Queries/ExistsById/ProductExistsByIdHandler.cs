using CustomCADs.Catalog.Domain.Products.Reads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public class ProductExistsByIdHandler(IProductReads reads) : IRequestHandler<ProductExistsByIdQuery, bool>
{
    public async Task<bool> Handle(ProductExistsByIdQuery req, CancellationToken ct)
    {
        bool productExists = await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);

        return productExists;
    }
}
