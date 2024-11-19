using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Gallery.Application.Carts.Commands.AddItem;

public class AddCartItemHandler(ICartReads reads, IUnitOfWork uow)
    : ICommandHandler<AddCartItemCommand>
{
    public async Task Handle(AddCartItemCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw CartNotFoundException.ById(req.Id);

        cart.AddItem(req.DeliveryType, req.Price, req.Quantity, req.ProductId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
