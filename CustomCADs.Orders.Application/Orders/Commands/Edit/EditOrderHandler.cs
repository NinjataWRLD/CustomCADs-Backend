using CustomCADs.Orders.Application.Orders.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

public class EditOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<EditOrderCommand>
{
    public async Task Handle(EditOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw OrderAuthorizationException.ByOrderId(req.Id);
        }

        order
            .SetName(req.Name)
            .SetDescription(req.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
