using CustomCADs.Orders.Domain.Common;

namespace CustomCADs.Orders.Application.Orders.Commands.CreateWithDelivery;

public sealed class CreateOrderWithDeliveryHandler(IWrites<Order> writes, IUnitOfWork uow)
    : ICommandHandler<CreateOrderWithDeliveryCommand, OrderId>
{
    public async Task<OrderId> Handle(CreateOrderWithDeliveryCommand req, CancellationToken ct)
    {
        Order order = req.ToOrder();

        await writes.AddAsync(order, ct);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
