using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedUrlsRequest, DownloadFileResponse>
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
        DownloadFileResponse response = await sender.SendQueryAsync(
            new GalleryGetProductImagePresignedUrlGetQuery(
                Id: ProductId.New(req.Id)
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
