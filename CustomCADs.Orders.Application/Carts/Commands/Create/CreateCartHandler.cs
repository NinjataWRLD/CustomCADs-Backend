using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Common;

namespace CustomCADs.Orders.Application.Carts.Commands.Create;

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
