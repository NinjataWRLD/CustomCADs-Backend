using CustomCADs.Gallery.Application.Carts;
using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Common;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.Create;

public class CreateCartHandler(IWrites<Cart> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCartCommand, CartId>
{
    public async Task<CartId> Handle(CreateCartCommand req, CancellationToken ct)
    {
        Cart cart = req.ToCart();

        await writes.AddAsync(cart, ct);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
