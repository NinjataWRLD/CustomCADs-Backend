﻿using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;

public sealed class ClientGetOngoingOrderByIdHandler(IOngoingOrderReads reads, IRequestSender sender)
    : IQueryHandler<ClientGetOngoingOrderByIdQuery, ClientGetOngoingOrderByIdDto>
{
    public async Task<ClientGetOngoingOrderByIdDto> Handle(ClientGetOngoingOrderByIdQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw OngoingOrderAuthorizationException.ByOrderId(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(Id: order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return order.ToGetOrderByIdDto(timeZone);
    }
}
