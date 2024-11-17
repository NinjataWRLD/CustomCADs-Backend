using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.CustomOrders.Entities;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Create;

public class CreateCustomOrderHandler(IWrites<CustomOrder> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCustomOrderCommand, CustomOrderId>
{
    public async Task<CustomOrderId> Handle(CreateCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = req.ToCustomerOrder();

        await writes.AddAsync(order, ct);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
