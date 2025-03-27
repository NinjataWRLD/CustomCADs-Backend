using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetCadUrlPost;

public class GetOngoingOrderCadPresignedUrlPostHandler(IOngoingOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetOngoingOrderCadPresignedUrlPostQuery, GetOngoingOrderCadPresignedUrlPostDto>
{
    public async Task<GetOngoingOrderCadPresignedUrlPostDto> Handle(GetOngoingOrderCadPresignedUrlPostQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(order.Id);

        GetCadPresignedUrlPostByIdQuery cadQuery = new(
            Name: order.Name,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var (Key, Url) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        return new(
            GeneratedKey: Key,
            PresignedUrl: Url
        );
    }
}
