using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Commands.AddLike;

public sealed class AddProductLikeHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<AddProductLikeCommand>
{
    public async Task Handle(AddProductLikeCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.AddToLikeCount();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
