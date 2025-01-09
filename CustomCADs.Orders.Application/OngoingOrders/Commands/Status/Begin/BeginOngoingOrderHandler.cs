using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Begin;

public sealed class BeginOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<BeginOngoingOrderCommand>
{
    public async Task Handle(BeginOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
        {
            throw OngoingOrderAuthorizationException.NotAssociated(order.Id, "begin");
        }
        order.SetBegunStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

