using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;

public class DecreaseActiveCartItemQuantityHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<DecreaseActiveCartItemQuantityCommand, int>
{
    public async Task<int> Handle(DecreaseActiveCartItemQuantityCommand req, CancellationToken ct)
    {
        ActiveCartItem item = await reads.SingleAsync(req.BuyerId, req.ProductId, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.BuyerId, req.ProductId }); ;

        item.DecreaseQuantity(req.Amount);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Quantity;
    }
}
