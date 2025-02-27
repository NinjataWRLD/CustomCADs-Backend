using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Remove;

public sealed class RemoveActiveCartItemHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<RemoveActiveCartItemCommand>
{
    public async Task Handle(RemoveActiveCartItemCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw ActiveCartItemNotFoundException.ById(req.ProductId);

        cart.RemoveItem(item);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
