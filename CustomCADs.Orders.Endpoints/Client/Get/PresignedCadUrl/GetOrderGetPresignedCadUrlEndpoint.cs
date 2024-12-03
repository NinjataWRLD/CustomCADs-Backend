using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedCadUrl;

public sealed class GetOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedCadUrlRequest, GetOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("09. Download Cad")
            .WithDescription("Download the Cad for your Finished(!) Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetOrderGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetOrderCadPresignedUrlGetQuery query = new(
            Id: new OrderId(req.Id)
        );
        var cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
