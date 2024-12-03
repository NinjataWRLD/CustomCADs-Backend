using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;
using CustomCADs.Catalog.Endpoints.Products.Contributors;

namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.PresignedCadUrl;

public sealed class GetProductGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedCadUrlRequest, GetProductGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("07. Download Cad")
            .WithDescription("Download your Product's Cad by specifying the Product's Id")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetProductCadPresignedUrlGetQuery query = new(
            Id: new ProductId(req.Id),
            CreatorId: User.GetAccountId()
        );
        GetProductCadPresignedUrlGetDto cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
