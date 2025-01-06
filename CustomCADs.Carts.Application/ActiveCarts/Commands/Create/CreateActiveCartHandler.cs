using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Create;

public sealed class CreateActiveCartHandler(IActiveCartReads reads, IWrites<ActiveCart> writes, IUnitOfWork uow)
    : ICommandHandler<CreateActiveCartCommand, ActiveCartId>
{
    public async Task<ActiveCartId> Handle(CreateActiveCartCommand req, CancellationToken ct)
    {
        ActiveCart? cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);
        if (cart != null)
            throw ActiveCartAlreadyExistsException.ByBuyerId(req.BuyerId);

        cart = req.ToCart();

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
