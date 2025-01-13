using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.PresignedCadUrl;

public sealed class GetProductGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedCadUrlRequest, GetProductGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("07. Download Cad")
            .WithDescription("Download a Product's Cad")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetProductCadPresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id)
        );
        GetProductCadPresignedUrlGetDto cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
