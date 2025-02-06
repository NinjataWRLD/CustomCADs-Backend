using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Put.PresignedUrl.Image;

public sealed class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutPresignedUrlRequest, GetProductPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace/image");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("12. Change Image")
            .WithDescription("Change your Product's Image")
        );
    }

    public override async Task HandleAsync(GetProductPutPresignedUrlRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlPutQuery presignedUrlQuery = new(
            Id: ProductId.New(req.Id),
            ContentType: req.ContentType,
            FileName: req.FileName,
            CreatorId: User.GetAccountId()
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetProductPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
