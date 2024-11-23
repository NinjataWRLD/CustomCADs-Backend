using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;
using CustomCADs.Orders.Endpoints.Designer;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedCadUrl;

public class GetOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedCadUrlRequest, GetOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to download the Cad for my Order"));
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
