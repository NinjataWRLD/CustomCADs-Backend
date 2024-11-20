using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.DomainEvents;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Orders.Application.Orders.Commands.Delete;

public class DeleteOrderHandler(IOrderReads reads, IWrites<Order> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        writes.Remove(order);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new OrderDeletedDomainEvent(
            req.Id,
            order.Image.Key
        )).ConfigureAwait(false);
    }
}
