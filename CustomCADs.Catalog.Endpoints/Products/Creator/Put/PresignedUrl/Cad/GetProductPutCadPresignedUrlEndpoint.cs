using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPut;
using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Put.PresignedUrl.Cad;

public sealed class GetProductPutCadPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutCadPresignedUrlRequest, GetProductPutCadPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace/cad");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("12. Change Cad")
            .WithDescription("Change your Product's Cad")
        );
    }

    public override async Task HandleAsync(GetProductPutCadPresignedUrlRequest req, CancellationToken ct)
    {
        GetProductCadPresignedUrlPutQuery presignedUrlQuery = new(
            Id: ProductId.New(req.Id),
            ContentType: req.ContentType,
            FileName: req.FileName,
            CreatorId: User.GetAccountId()
        );
        var cadDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetProductPutCadPresignedUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
