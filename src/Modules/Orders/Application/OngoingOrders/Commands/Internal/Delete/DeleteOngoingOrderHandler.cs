﻿using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Delete;

public sealed class DeleteOngoingOrderHandler(IOngoingOrderReads reads, IWrites<OngoingOrder> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteOngoingOrderCommand>
{
    public async Task Handle(DeleteOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        writes.Remove(order);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
