﻿using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;

public sealed class ClientGetOngoingOrderByIdHandler(IOngoingOrderReads reads, IRequestSender sender)
    : IQueryHandler<ClientGetOngoingOrderByIdQuery, ClientGetOngoingOrderByIdDto>
{
    public async Task<ClientGetOngoingOrderByIdDto> Handle(ClientGetOngoingOrderByIdQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        GetTimeZoneByIdQuery timeZoneQuery = new(Id: order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        string? designer = null;
        if (order.DesignerId is not null)
        {
            GetUsernameByIdQuery designerQuery = new(order.DesignerId.Value);
            designer = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        }

        return order.ToClientGetByIdDto(timeZone, designer);
    }
}
