using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Get;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.PresignedUrl;

public sealed class GetMaterialPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialGetPresignedUrlRequest, DownloadFileResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download");
        Group<MaterialsGroup>();
        AllowAnonymous();
        Description(d => d
            .WithSummary("Download Texture")
            .WithDescription("Download your Material's Texture")
        );
    }

    public override async Task HandleAsync(GetMaterialGetPresignedUrlRequest req, CancellationToken ct)
    {
        GetMaterialTexturePresignedUrlGetQuery presignedUrlQuery = new(
            Id: MaterialId.New(req.Id)
        );
        var response = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);


        await SendOkAsync(response).ConfigureAwait(false);
    }
}
