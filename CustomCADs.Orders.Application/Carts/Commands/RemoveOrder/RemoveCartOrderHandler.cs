using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Application.Carts.Commands.RemoveOrder;

public class RemoveCartOrderHandler(ICartReads reads, IUnitOfWork uow)
    : ICommandHandler<RemoveCartOrderCommand>
{
    public async Task Handle(RemoveCartOrderCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        cart.RemoveOrder(req.OrderId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
