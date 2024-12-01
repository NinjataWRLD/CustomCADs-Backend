using CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public class GetOrderPatchPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderPatchPresignedCadUrlRequest, GetOrderPatchPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/upload");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("08. Upload Cad")
            .WithDescription("Upload a Cad before finishing an Order if it's set for Digital Delivery")
        );
    }

    public override async Task HandleAsync(GetOrderPatchPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetOrderCadPresignedUrlPostQuery query = new(
            OrderName: req.OrderName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        GetOrderCadPresignedUrlPostDto dto = await sender
            .SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderPatchPresignedCadUrlResponse response = new(
            GeneratedCadKey: dto.PresignedKey,
            PresignedCadUrl: dto.GeneratedUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
