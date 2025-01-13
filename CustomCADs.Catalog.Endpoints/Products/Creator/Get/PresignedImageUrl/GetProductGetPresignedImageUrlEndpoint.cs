using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.PresignedImageUrl;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedImageUrlRequest, GetProductGetPresignedImageUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/image");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("11. Download Image")
            .WithDescription("Download an Product's Image by specifying the Product's Id")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id)
        );
        GetProductImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedImageUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
