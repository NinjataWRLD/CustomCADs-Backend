using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.PresignedCadUrl;

public sealed class GetOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedCadUrlRequest, GetOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("12. Download Cad")
            .WithDescription("Download the Cad for your Completed(!) Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetOrderGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetOrderCadPresignedUrlGetQuery query = new(
            Id: new OrderId(req.Id),
            BuyerId: User.GetAccountId()
        );
        var cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
