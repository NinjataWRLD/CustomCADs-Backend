using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedCadUrl;

public class GetOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedCadUrlRequest, GetOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("08. Download Cad")
            .WithDescription("Download the Cad for your Finished(!) Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetOrderGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        GetOrderCadPresignedUrlGetQuery query = new(id);
        var cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderGetPresignedCadUrlResponse response = new(cadDto.CadUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
