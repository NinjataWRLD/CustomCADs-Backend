using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Begin;

public class BeginCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<BeginCustomOrderCommand>
{
    public async Task Handle(BeginCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        if (req.BeginnerId != order.DesignerId)
        {
            throw CustomOrderValidationException.Custom("Cannot begin an order you aren't associated with.");
        }
        order.SetBegunStatus();

        await uow.SaveChangesAsync(ct);
    }
}

