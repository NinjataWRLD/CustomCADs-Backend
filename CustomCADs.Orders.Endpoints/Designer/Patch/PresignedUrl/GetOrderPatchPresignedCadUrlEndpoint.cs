using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public class GetOrderPatchPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderPatchPresignedCadUrlRequest, GetOrderPatchPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/upload");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to upload a Cad for the Order"));
    }

    public override async Task HandleAsync(GetOrderPatchPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetOrderCadPresignedUrlPostQuery query = new(
            OrderName: req.OrderName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderPatchPresignedCadUrlResponse response = new(
            GeneratedCadKey: dto.CadKey,
            PresignedCadUrl: dto.CadUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
