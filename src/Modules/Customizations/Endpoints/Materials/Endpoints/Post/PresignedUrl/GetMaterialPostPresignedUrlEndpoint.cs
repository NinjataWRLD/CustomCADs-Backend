using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Post.PresignedUrl;

public sealed class GetMaterialPostPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialPostPresignedUrlRequest, UploadFileResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload");
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("Upload Texture")
            .WithDescription("Upload your Material's Texture")
        );
    }

    public override async Task HandleAsync(GetMaterialPostPresignedUrlRequest req, CancellationToken ct)
    {
        GetMaterialTexturePresignedUrlPostQuery presignedUrlQuery = new(
            MaterialName: req.MaterialName,
            Image: req.Image
        );
        UploadFileResponse response = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
