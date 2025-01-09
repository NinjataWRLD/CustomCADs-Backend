using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;

public sealed class EditOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<EditOngoingOrderCommand>
{
    public async Task Handle(EditOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw OngoingOrderAuthorizationException.ByOrderId(req.Id);
        }

        order
            .SetName(req.Name)
            .SetDescription(req.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
