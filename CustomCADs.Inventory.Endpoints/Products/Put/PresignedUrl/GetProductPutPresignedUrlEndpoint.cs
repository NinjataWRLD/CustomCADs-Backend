using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPut;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Endpoints.Products.Put.PresignedUrl;

public class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutPresignedUrlRequest, GetProductPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("10. I want to replace the image for my Product"));
    }

    public override async Task HandleAsync(GetProductPutPresignedUrlRequest req, CancellationToken ct)
    {
        ProductId id = new(req.Id);
        GetProductByIdQuery productQuery = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(productQuery, ct).ConfigureAwait(false);

        GetProductImagePresignedUrlPutQuery presignedUrlQuery = new(
            ImageKey: product.Image.Key,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetProductPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
