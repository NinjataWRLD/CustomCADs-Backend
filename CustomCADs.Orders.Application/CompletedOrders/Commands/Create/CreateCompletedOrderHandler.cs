﻿using CustomCADs.Orders.Domain.Common;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Create;

public class CreateCompletedOrderHandler(IWrites<CompletedOrder> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCompletedOrderCommand, CompletedOrderId>
{
    public async Task<CompletedOrderId> Handle(CreateCompletedOrderCommand req, CancellationToken ct)
    {
        CompletedOrder order = req.ToCompletedOrder();

        await writes.AddAsync(order, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
