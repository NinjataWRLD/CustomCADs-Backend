using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.SetDelivery;

public class SetOngoingOrderDeliveryHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<SetOngoingOrderDeliveryCommand>
{
    public async Task Handle(SetOngoingOrderDeliveryCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        order.SetDelivery(req.Value);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
