﻿using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Create;

public sealed class CreateOngoingOrderHandler(IWrites<OngoingOrder> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateOngoingOrderCommand, OngoingOrderId>
{
    public async Task<OngoingOrderId> Handle(CreateOngoingOrderCommand req, CancellationToken ct)
    {
        GetAccountExistsByIdQuery buyerQuery = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        if (!buyerExists)
            throw CustomNotFoundException<OngoingOrder>.ById(req.BuyerId, "User");

        OngoingOrder order = req.ToEntity();

        await writes.AddAsync(order, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
