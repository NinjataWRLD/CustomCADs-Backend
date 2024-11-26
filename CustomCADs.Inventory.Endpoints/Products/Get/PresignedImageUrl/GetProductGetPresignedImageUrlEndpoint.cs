using CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

namespace CustomCADs.Inventory.Endpoints.Products.Get.PresignedImageUrl;

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
        GetProductImagePresignedUrlGetQuery query = new(
            Id: new(req.Id),
            CreatorId: User.GetAccountId()
        );
        GetProductImagePresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedImageUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
