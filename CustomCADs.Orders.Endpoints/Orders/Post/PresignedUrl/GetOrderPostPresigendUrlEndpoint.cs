using CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPost;

namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public class GetOrderPostPresigendUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderPostPresigendUrlRequest, GetOrderPostPresigendUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/upload");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("1. I want to upload the Image for my Order"));
    }

    public override async Task HandleAsync(GetOrderPostPresigendUrlRequest req, CancellationToken ct)
    {
        GetOrderImagePresignedUrlPostQuery query = new(
            OrderName: req.OrderName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderPostPresigendUrlResponse response = new(
            GeneratedImageKey: dto.GeneratedKey,
            PresignedImageUrl: dto.PresignedUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
