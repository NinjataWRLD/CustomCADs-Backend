using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Accept;

public class AcceptCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<AcceptCustomOrderCommand>
{
    public async Task Handle(AcceptCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        order.SetAcceptedStatus();
        order.SetDesignerId(req.DesignerId);

        await uow.SaveChangesAsync(ct);
    }
}

