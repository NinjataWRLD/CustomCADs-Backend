using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;
using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPost;

namespace CustomCADs.Catalog.Endpoints.Products.Post.PresignedUrls;

public sealed class GetProductPostPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductPostPresignedUrlsRequest, GetProductPostPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("01. Upload Image & Cad")
            .WithDescription("Upload the Image and Cad for a Product (prior to creating it)")
        );
    }

    public override async Task HandleAsync(GetProductPostPresignedUrlsRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlPostQuery imageQuery = new(
            ProductName: req.ProductName,
            ContentType: req.ImageContentType,
            FileName: req.ImageFileName
        );
        var imageDto = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        GetProductCadPresignedUrlPostQuery cadQuery = new(
            ProductName: req.ProductName,
            ContentType: req.CadContentType,
            FileName: req.CadFileName
        );
        var cadDto = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        GetProductPostPresignedUrlsResponse response = new(
            GeneratedImageKey: imageDto.GeneratedKey,
            PresignedImageUrl: imageDto.PresignedUrl,
            GeneratedCadKey: cadDto.CadKey,
            PresignedCadUrl: cadDto.CadUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
