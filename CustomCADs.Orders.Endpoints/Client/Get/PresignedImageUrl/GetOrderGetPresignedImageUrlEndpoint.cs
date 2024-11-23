using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedImageUrl;

public class GetOrderGetPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedImageUrlRequest, GetOrderGetPresignedUrlImageResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadImage");
        Group<ClientGroup>();
        Description(d => d.WithSummary("I want to download my Order's Image"));
    }

    public override async Task HandleAsync(GetOrderGetPresignedImageUrlRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        GetOrderImagePresignedUrlGetQuery imageQuery = new(id);
        var imageDto = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        GetOrderGetPresignedUrlImageResponse response = new(imageDto.ImageUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
