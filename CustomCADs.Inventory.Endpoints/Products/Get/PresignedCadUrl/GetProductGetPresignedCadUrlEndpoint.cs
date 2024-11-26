using CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

namespace CustomCADs.Inventory.Endpoints.Products.Get.PresignedCadUrl;

public class GetProductGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedCadUrlRequest, GetProductGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("I want to download the Cad for my Product"));
    }

    public override async Task HandleAsync(GetProductGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetProductCadPresignedUrlGetQuery query = new(
            Id: new(req.Id),
            CreatorId: User.GetAccountId()
        );
        GetProductCadPresignedUrlGetDto imageDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedCadUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
