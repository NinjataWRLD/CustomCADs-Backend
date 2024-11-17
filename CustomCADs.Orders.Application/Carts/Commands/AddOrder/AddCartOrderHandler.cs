using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Application.Carts.Commands.AddOrder;

public class AddCartOrderHandler(ICartReads reads, IUnitOfWork uow)
    : ICommandHandler<AddCartOrderCommand>
{
    public async Task Handle(AddCartOrderCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw CartNotFoundException.ById(req.Id);

        cart.AddOrder(req.DeliveryType, req.Price, req.Quantity, req.ProductId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
