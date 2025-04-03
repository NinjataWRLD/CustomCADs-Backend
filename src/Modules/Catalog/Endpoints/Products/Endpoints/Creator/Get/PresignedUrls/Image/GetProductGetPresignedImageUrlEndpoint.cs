using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedImageUrlRequest, GetProductGetPresignedImageUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/image");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Download Image")
            .WithDescription("Download an Product's Image")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
    {
        CreatorGetProductImagePresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id),
            CreatorId: User.GetAccountId()
        );
        CreatorGetProductImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedImageUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
