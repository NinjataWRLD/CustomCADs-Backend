﻿using CustomCADs.Orders.Application.Orders.Exceptions;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public class GetOrderCadPresignedUrlGetHandler(IOrderReads reads, IStorageService storage, IRequestSender sender)
    : IQueryHandler<GetOrderCadPresignedUrlGetQuery, GetOrderCadPresignedUrlGetDto>
{
    public async Task<GetOrderCadPresignedUrlGetDto> Handle(GetOrderCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.CadId is null)
        {
            throw OrderValidationException.CannotGetOrderCadWithoutCadId();
        }
        GetCadByIdQuery cadQuery = new(order.CadId.Value);
        var (Key, ContentType, _, _) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        string cadUrl = await storage.GetPresignedGetUrlAsync(
            key: Key,
            contentType: ContentType
        ).ConfigureAwait(false);

        GetOrderCadPresignedUrlGetDto response = new(cadUrl);
        return response;
    }
}
