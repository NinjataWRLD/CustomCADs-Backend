using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.Carts.Commands.RemoveItem;

public sealed class RemoveCartItemHandler(ICartReads reads, IUnitOfWork uow)
    : ICommandHandler<RemoveCartItemCommand>
{
    public async Task Handle(RemoveCartItemCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        cart.RemoveItem(req.ItemId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
