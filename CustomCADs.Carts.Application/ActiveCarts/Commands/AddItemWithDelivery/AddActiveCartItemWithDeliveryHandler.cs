using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItemWithDelivery;

public sealed class AddActiveCartItemWithDeliveryHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<AddActiveCartItemWithDeliveryCommand, ActiveCartItemId>
{
    public async Task<ActiveCartItemId> Handle(AddActiveCartItemWithDeliveryCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.AddItem(
            productId: req.ProductId,
            weight: req.Weight,
            delivery: true
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
