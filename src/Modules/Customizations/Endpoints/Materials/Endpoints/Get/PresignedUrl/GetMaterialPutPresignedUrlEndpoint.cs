using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Get;
using CustomCADs.Customizations.Endpoints.Materials.Endpoints;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.PresignedUrl;

public sealed class GetMaterialPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialGetPresignedUrlRequest, GetMaterialGetPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download");
        Group<MaterialsGroup>();
        AllowAnonymous();
        Description(d => d
            .WithSummary("12. Download Texture")
            .WithDescription("Download your Material's Texture")
        );
    }

    public override async Task HandleAsync(GetMaterialGetPresignedUrlRequest req, CancellationToken ct)
    {
        GetMaterialTexturePresignedUrlGetQuery presignedUrlQuery = new(
            Id: MaterialId.New(req.Id)
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetMaterialGetPresignedUrlResponse response = new(
            imageDto.PresignedUrl,
            imageDto.ContentType
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
