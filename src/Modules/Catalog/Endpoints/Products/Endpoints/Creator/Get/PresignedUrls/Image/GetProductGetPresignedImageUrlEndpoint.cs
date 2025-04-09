using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedImageUrlRequest, DownloadFileResponse>
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
        DownloadFileResponse response = await sender.SendQueryAsync(
            new CreatorGetProductImagePresignedUrlGetQuery(
                Id: ProductId.New(req.Id),
                CreatorId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
