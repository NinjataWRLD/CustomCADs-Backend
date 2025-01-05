using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItem;

public sealed class AddActiveCartItemHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<AddActiveCartItemCommand, ActiveCartItemId>
{
    public async Task<ActiveCartItemId> Handle(AddActiveCartItemCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        double weight = req.Weight;
        ActiveCartItem item = cart.AddItem(
            productId: req.ProductId,
            weight: weight,
            delivery: false
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
