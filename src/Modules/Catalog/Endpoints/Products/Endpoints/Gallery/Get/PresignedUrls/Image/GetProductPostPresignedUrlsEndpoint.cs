using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Get;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedUrlsRequest, GetProductGetPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/image");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("Download Image")
            .WithDescription("Download the Image for a Product")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedUrlsRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id)
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedUrlsResponse response = new(
            PresignedUrl: dto.PresignedUrl,
            ContentType: dto.ContentType
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
