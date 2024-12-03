using CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

namespace CustomCADs.Inventory.Endpoints.Products.Get.PresignedImageUrl;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedImageUrlRequest, GetProductGetPresignedImageUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadImage");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("11. Download Image")
            .WithDescription("Download your Product's Image by specifying the Product's Id")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlGetQuery query = new(
            Id: new ProductId(req.Id),
            CreatorId: User.GetAccountId()
        );
        GetProductImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedImageUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
