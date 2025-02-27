using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;

public class ToggleActiveCartItemForDeliveryHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<ToggleActiveCartItemForDeliveryCommand>
{
    public async Task Handle(ToggleActiveCartItemForDeliveryCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.Items.SingleOrDefault(i => i.ProductId == req.ProductId)
            ?? throw ActiveCartItemNotFoundException.ById(req.ProductId);

        item.SetForDelivery(!item.ForDelivery);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
