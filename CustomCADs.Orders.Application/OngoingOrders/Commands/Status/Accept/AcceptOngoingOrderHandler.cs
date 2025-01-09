using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Accept;

public sealed class AcceptOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<AcceptOngoingOrderCommand>
{
    public async Task Handle(AcceptOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        order.SetAcceptedStatus();
        order.SetDesignerId(req.DesignerId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

