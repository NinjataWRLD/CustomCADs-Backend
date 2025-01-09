using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Report;

public sealed class ReportOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<ReportOngoingOrderCommand>
{
    public async Task Handle(ReportOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        order.SetReportedStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

