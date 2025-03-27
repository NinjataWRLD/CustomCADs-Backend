using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Quantity.Increment;

public class IncreaseActiveCartItemQuantityHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<IncreaseActiveCartItemQuantityCommand, int>
{
    public async Task<int> Handle(IncreaseActiveCartItemQuantityCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);

        ActiveCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw CustomNotFoundException<ActiveCartItem>.ById(req.ProductId);

        item.IncreaseQuantity(req.Amount);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Quantity;
    }
}
