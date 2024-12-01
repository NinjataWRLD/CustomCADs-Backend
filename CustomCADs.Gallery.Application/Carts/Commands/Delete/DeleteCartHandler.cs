using CustomCADs.Gallery.Application.Common.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common;

namespace CustomCADs.Gallery.Application.Carts.Commands.Delete;

public class DeleteCartHandler(ICartReads reads, IWrites<Cart> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteCartCommand>
{
    public async Task Handle(DeleteCartCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId == req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        writes.Remove(cart);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
