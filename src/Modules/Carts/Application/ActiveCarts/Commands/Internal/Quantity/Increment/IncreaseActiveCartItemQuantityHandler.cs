using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;

public class IncreaseActiveCartItemQuantityHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<IncreaseActiveCartItemQuantityCommand, int>
{
    public async Task<int> Handle(IncreaseActiveCartItemQuantityCommand req, CancellationToken ct)
    {
        ActiveCartItem item = await reads.SingleAsync(req.BuyerId, req.ProductId, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.BuyerId, req.ProductId }); ;

        item.IncreaseQuantity(req.Amount);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Quantity;
    }
}
