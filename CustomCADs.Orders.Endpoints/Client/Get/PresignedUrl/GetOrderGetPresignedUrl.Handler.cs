using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedUrl;

public class GetOrderGetPresignedUrlHandler(IRequestSender sender)
    : Endpoint<GetOrderGetPresignedUrlRequest, GetOrderGetPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/download");
        Group<ClientGroup>();
        Description(d => d.WithSummary("I want to download my Order's Image"));
    }

    public override async Task HandleAsync(GetOrderGetPresignedUrlRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        GetOrderByIdQuery orderQuery = new(id);
        GetOrderByIdDto order = await sender.SendQueryAsync(orderQuery, ct).ConfigureAwait(false);

        GetOrderImagePresignedUrlGetQuery imageQuery = new(order.Image.Key, order.Image.ContentType);
        GetOrderImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        GetOrderGetPresignedUrlResponse response = new(imageDto.ImageUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
