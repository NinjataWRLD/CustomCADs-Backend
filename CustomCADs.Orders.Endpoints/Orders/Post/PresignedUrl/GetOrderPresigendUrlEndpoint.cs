using CustomCADs.Orders.Application.Orders.Queries.GetImageUrl;

namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public class GetOrderPresigendUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderPresigendUrlRequest, GetOrderPresigendUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("1. I want to upload the Image for my Order"));
    }

    public override async Task HandleAsync(GetOrderPresigendUrlRequest req, CancellationToken ct)
    {
        GetOrderImagePresignedUrlQuery query = new(
            OrderName: req.OrderName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderPresigendUrlResponse response = new(
            GeneratedImageKey: dto.ImageKey,
            PresignedImageUrl: dto.ImageUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
