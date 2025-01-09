using CustomCADs.Orders.Domain.Common;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Create;

public sealed class CreateOngoingOrderHandler(IWrites<OngoingOrder> writes, IUnitOfWork uow)
    : ICommandHandler<CreateOngoingOrderCommand, OngoingOrderId>
{
    public async Task<OngoingOrderId> Handle(CreateOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = req.ToOngoingOrder();

        await writes.AddAsync(order, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
