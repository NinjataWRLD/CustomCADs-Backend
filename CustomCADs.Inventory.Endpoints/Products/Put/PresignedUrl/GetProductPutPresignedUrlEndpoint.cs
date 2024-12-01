using CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPut;

namespace CustomCADs.Inventory.Endpoints.Products.Put.PresignedUrl;

public class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutPresignedUrlRequest, GetProductPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("12. Change Image")
            .WithDescription("Change your Product's Image by specifying the Product's Id and the Image's FileName and ContentType")
        );
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
