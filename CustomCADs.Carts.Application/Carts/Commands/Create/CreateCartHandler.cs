using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.Carts.Commands.Create;

public sealed class CreateCartHandler(IWrites<Cart> writes, IUnitOfWork uow)
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
