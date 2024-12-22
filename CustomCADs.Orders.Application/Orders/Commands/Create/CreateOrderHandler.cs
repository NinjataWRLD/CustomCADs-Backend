using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public sealed class CreateOrderHandler(IWrites<Order> writes, IUnitOfWork uow)
    : ICommandHandler<CreateOrderCommand, OrderId>
{
    public async Task<OrderId> Handle(CreateOrderCommand req, CancellationToken ct)
    {
        Order order = req.ToOrder();

        await writes.AddAsync(order, ct);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
