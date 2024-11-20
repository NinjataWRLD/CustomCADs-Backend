using CustomCADs.Catalog.Application.Products.Queries.GetCadUrl;
using CustomCADs.Catalog.Application.Products.Queries.GetImageUrl;

namespace CustomCADs.Catalog.Endpoints.Products.Post.PresignedUrls;

public class GetProductPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductPresignedUrlsRequest, GetProductPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("predesignedUrls");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("1. I want to upload the Image and Cad for my Product"));
    }

    public override async Task HandleAsync(GetProductPresignedUrlsRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlQuery imageQuery = new(
            ProductName: req.ProductName,
            ContentType: req.ImageContentType,
            FileName: req.ImageFileName
        );
        var imageDto = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        GetProductCadPresignedUrlQuery cadQuery = new(
            ProductName: req.ProductName,
            ContentType: req.CadContentType,
            FileName: req.CadFileName
        );
        var cadDto = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        GetProductPresignedUrlsResponse response = new(
            GeneratedImageKey: imageDto.ImageKey,
            PresignedImageUrl: imageDto.ImageUrl,
            GeneratedCadKey: cadDto.CadKey,
            PresignedCadUrl: cadDto.CadUrl
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
