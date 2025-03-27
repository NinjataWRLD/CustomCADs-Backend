using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Get;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.PresignedUrls.Cad;

public sealed class GetProductGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedCadUrlRequest, GetProductGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Download Cad")
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
