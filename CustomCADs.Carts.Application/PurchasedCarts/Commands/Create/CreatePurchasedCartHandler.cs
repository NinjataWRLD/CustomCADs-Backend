
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;

public class CreatePurchasedCartHandler(IWrites<PurchasedCart> writes, IUnitOfWork uow)
    : ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
    public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
    {
        var cart = PurchasedCart.Create(req.BuyerId);

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
