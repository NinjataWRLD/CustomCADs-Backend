using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;

public sealed class GetCompletedOrderCadPresignedUrlGetHandler(ICompletedOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetCompletedOrderCadPresignedUrlGetQuery, GetCompletedOrderCadPresignedUrlGetDto>
{
    public async Task<GetCompletedOrderCadPresignedUrlGetDto> Handle(GetCompletedOrderCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        CompletedOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<CompletedOrder>.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<CompletedOrder>.Custom($"Cannot access another Buyer's Completed Order: {order.Id}.");

        GetCadPresignedUrlGetByIdQuery query = new(order.CadId);
        var (Url, ContetType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContetType
        );
    }
}
