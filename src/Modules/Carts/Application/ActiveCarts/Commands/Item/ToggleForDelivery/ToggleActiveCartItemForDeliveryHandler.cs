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
            ?? throw ActiveCartItemNotFoundException.ByProductId(req.ProductId);

        if (item.ForDelivery)
        {
            item.SetNoDelivery();
        }
        else if (req.CustomizationId is not null)
        {
            item.SetForDelivery(req.CustomizationId.Value);
        }
        else throw ActiveCartItemDeliveryException.ById(cart.Id);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
