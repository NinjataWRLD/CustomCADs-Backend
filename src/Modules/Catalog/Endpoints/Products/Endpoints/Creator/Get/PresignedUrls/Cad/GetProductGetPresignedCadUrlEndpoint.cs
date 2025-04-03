using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;

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
        CreatorGetProductCadPresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id),
            CreatorId: User.GetAccountId()
        );
        CreatorGetProductCadPresignedUrlGetDto cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
