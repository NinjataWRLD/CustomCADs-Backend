using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Cad;

public sealed class GetProductPutCadPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetProductPutCadPresignedUrlRequest, GetProductPutCadPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace/cad");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Change Cad")
            .WithDescription("Change your Product's Cad")
        );
    }

    public override async Task HandleAsync(GetProductPutCadPresignedUrlRequest req, CancellationToken ct)
    {
        CreatorGetProductCadPresignedUrlPutQuery presignedUrlQuery = new(
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
