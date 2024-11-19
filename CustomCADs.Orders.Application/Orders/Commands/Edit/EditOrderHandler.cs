using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders.Entities;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

public class EditOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<EditOrderCommand>
{
    public async Task Handle(EditOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        order.SetName(req.Name ?? order.Name);
        order.SetDescription(req.Description ?? order.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
