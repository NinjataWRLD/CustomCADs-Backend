using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.PresignedUrls;

public sealed class GetProductPostPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductPostPresignedUrlsRequest, GetProductPostPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Upload Image & Cad")
            .WithDescription("Upload the Image and Cad for a Product")
        );
    }

    public override async Task HandleAsync(GetProductPostPresignedUrlsRequest req, CancellationToken ct)
    {
        CreatorGetProductImagePresignedUrlPostQuery imageQuery = new(
            ProductName: req.ProductName,
            ContentType: req.ImageContentType,
            FileName: req.ImageFileName
        );
        var imageDto = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        CreatorGetProductCadPresignedUrlPostQuery cadQuery = new(
            ProductName: req.ProductName,
            ContentType: req.CadContentType,
            FileName: req.CadFileName
        );
        var cadDto = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        GetProductPostPresignedUrlsResponse response = new(
            GeneratedImageKey: imageDto.GeneratedKey,
            PresignedImageUrl: imageDto.PresignedUrl,
            GeneratedCadKey: cadDto.GeneratedKey,
            PresignedCadUrl: cadDto.PresignedUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
