using CustomCADs.Carts.Application.Common.Exceptions;
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

        cart.RemoveItem(req.ItemId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
