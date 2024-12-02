using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Products.Commands.AddPurchase;

namespace CustomCADs.Inventory.Application.Products.SharedCommandHandlers;

public class AddProductPurchaseHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<AddProductPurchaseCommand>
{
    public async Task Handle(AddProductPurchaseCommand req, CancellationToken ct)
    {
        ProductQuery query = new(Ids: req.Ids);
        Result<Product> result = await reads.AllAsync(query, ct: ct).ConfigureAwait(false);

        foreach (Product product in result.Items)
            product.AddToPurchaseCount();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
