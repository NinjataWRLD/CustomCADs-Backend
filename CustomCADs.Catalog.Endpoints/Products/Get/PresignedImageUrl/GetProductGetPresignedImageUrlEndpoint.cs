using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

namespace CustomCADs.Catalog.Endpoints.Products.Get.PresignedImageUrl;

public class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedImageUrlRequest, GetProductGetPresignedImageUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadImage");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("I want to download the Image for my Product"));
    }

    public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
    {
        ProductId id = new(req.Id);
        GetProductImagePresignedUrlGetQuery query = new(id);
        GetProductImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedImageUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
