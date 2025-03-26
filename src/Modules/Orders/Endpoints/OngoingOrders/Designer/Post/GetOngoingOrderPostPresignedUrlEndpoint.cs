using CustomCADs.Orders.Application.OngoingOrders.Queries.GetCadUrlPost;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Post;

public sealed class GetOngoingOrderPostPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOngoingOrderPostPresignedUrlRequest, GetOngoingOrderPostPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload/cad");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("09. Upload")
            .WithDescription("Upload a Cad for an Ongoing Order")
        );
    }

    public override async Task HandleAsync(GetOngoingOrderPostPresignedUrlRequest req, CancellationToken ct)
    {
        GetOngoingOrderCadPresignedUrlPostQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            ContentType: req.ContentType,
            FileName: req.FileName,
            DesignerId: User.GetAccountId()
        );
        GetOngoingOrderCadPresignedUrlPostDto dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = dto.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
