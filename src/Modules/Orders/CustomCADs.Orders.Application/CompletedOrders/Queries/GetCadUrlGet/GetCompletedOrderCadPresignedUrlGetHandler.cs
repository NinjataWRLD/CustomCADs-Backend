using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;

public sealed class GetCompletedOrderCadPresignedUrlGetHandler(ICompletedOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetCompletedOrderCadPresignedUrlGetQuery, GetCompletedOrderCadPresignedUrlGetDto>
{
    public async Task<GetCompletedOrderCadPresignedUrlGetDto> Handle(GetCompletedOrderCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        CompletedOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CompletedOrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw CompletedOrderAuthorizationException.ByOrderId(req.Id);
        }

        GetCadPresignedUrlGetByIdQuery query = new(order.CadId);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
