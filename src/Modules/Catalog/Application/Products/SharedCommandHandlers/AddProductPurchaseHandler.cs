using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Products.Commands;

namespace CustomCADs.Catalog.Application.Products.SharedCommandHandlers;

public sealed class AddProductPurchaseHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<AddProductPurchaseCommand>
{
    public async Task Handle(AddProductPurchaseCommand req, CancellationToken ct)
    {
        ProductQuery query = new(
            Ids: req.Ids,
            Pagination: new(Limit: req.Ids.Length)
        );
        Result<Product> result = await reads.AllAsync(query, ct: ct).ConfigureAwait(false);

        foreach (Product product in result.Items)
            product.AddToPurchaseCount();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
