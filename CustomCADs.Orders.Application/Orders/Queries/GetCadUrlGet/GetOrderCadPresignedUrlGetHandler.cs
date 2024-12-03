using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public sealed class GetOrderCadPresignedUrlGetHandler(IOrderReads reads, IStorageService storage, IRequestSender sender)
    : IQueryHandler<GetOrderCadPresignedUrlGetQuery, GetOrderCadPresignedUrlGetDto>
{
    public async Task<GetOrderCadPresignedUrlGetDto> Handle(GetOrderCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.CadId is null)
        {
            throw OrderCadException.ById(req.Id);
        }

        if (order.OrderStatus != OrderStatus.Completed)
        {
            throw OrderStatusException.ById(req.Id, OrderStatus.Completed);
        }

        GetCadByIdQuery cadQuery = new(order.CadId.Value);
        var (Key, ContentType, _, _) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        string cadUrl = await storage.GetPresignedGetUrlAsync(
            key: Key,
            contentType: ContentType
        ).ConfigureAwait(false);

        return new(PresignedUrl: cadUrl);
    }
}
