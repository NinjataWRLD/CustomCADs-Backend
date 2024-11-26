using CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPut;

namespace CustomCADs.Inventory.Endpoints.Products.Put.PresignedUrl;

public class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutPresignedUrlRequest, GetProductPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("10. I want to replace the image for my Product"));
    }

    public override async Task HandleAsync(GetProductPutPresignedUrlRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlPutQuery presignedUrlQuery = new(
            Id: new(req.Id),
            ContentType: req.ContentType,
            FileName: req.FileName,
            CreatorId: User.GetAccountId()
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetProductPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
