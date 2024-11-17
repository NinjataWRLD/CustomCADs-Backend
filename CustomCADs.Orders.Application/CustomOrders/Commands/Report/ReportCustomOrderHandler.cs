using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Report;

public class ReportCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<ReportCustomOrderCommand>
{
    public async Task Handle(ReportCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        order.SetReportedStatus();

        await uow.SaveChangesAsync(ct);
    }
}

