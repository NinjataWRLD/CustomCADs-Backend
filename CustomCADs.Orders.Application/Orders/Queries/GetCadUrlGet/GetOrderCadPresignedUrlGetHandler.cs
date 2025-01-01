using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public sealed class GetOrderCadPresignedUrlGetHandler(IOrderReads reads, IRequestSender sender)
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

        GetCadPresignedUrlGetByIdQuery query = new(order.CadId.Value);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
