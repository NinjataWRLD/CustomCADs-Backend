using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Report;

public sealed class ReportOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<ReportOngoingOrderCommand>
{
    public async Task Handle(ReportOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.OrderStatus is OngoingOrderStatus.Pending)
        {
            order.SetDesignerId(req.DesignerId);
        }
        else if (order.DesignerId != req.DesignerId)
        {
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);
        }

        order.SetReportedStatus();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

