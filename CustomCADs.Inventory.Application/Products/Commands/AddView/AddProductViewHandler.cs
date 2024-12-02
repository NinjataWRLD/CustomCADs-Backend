using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Commands.AddView;

public class AddProductViewHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<AddProductViewCommand>
{
    public async Task Handle(AddProductViewCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.AddToViewCount();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
