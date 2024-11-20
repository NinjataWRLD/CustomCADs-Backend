using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPut;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Orders.Put.PresignedUrl;

public class GetOrderPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetOrderPutPresignedUrlRequest, GetOrderPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/replace");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("9. I want to replace the image for my Order"));
    }

    public override async Task HandleAsync(GetOrderPutPresignedUrlRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        GetOrderByIdQuery orderQuery = new(id);
        GetOrderByIdDto order = await sender.SendQueryAsync(orderQuery, ct).ConfigureAwait(false);

        GetOrderImagePresignedUrlPutQuery presignedUrlQuery = new(
            ImageKey: order.Image.Key,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetOrderPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
