using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Quantity.Decrement;

public class DecreaseActiveCartItemQuantityHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<DecreaseActiveCartItemQuantityCommand, int>
{
    public async Task<int> Handle(DecreaseActiveCartItemQuantityCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.Items.SingleOrDefault(i => i.ProductId == req.ProductId)
            ?? throw ActiveCartItemNotFoundException.ByProductId(req.ProductId);

        item.DecreaseQuantity(req.Amount);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Quantity;
    }
}
