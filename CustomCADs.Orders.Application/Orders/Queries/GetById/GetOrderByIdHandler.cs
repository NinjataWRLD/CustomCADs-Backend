﻿using CustomCADs.Orders.Application.Orders.Exceptions;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public class GetOrderByIdHandler(IOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdDto>
{
    public async Task<GetOrderByIdDto> Handle(GetOrderByIdQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.BuyerId == req.BuyerId)
        {
            throw OrderAuthorizationException.ByOrderId(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return order.ToGetOrderByIdDto(timeZone);
    }
}
