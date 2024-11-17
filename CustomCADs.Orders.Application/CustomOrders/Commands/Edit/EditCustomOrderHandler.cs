using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Edit;

public class EditCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<EditCustomOrderCommand>
{
    public async Task Handle(EditCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        order.SetName(req.Name ?? order.Name);
        order.SetDescription(req.Description ?? order.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
