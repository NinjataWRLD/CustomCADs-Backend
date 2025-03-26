using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetCadUrlGet;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.PresignedCadUrl;

public sealed class GetCompletedOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrderGetPresignedCadUrlRequest, GetCompletedOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Download Cad")
            .WithDescription("Download the Cad for your Completed Order")
        );
    }

    public override async Task HandleAsync(GetCompletedOrderGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: CompletedOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        var cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCompletedOrderGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
