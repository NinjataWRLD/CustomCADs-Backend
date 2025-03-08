using CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Post;

namespace CustomCADs.Customizations.Endpoints.Materials.Post.PresignedUrl;

public sealed class GetMaterialPostPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialPostPresignedUrlRequest, GetMaterialPostPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload");
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("12. Upload Texture")
            .WithDescription("Upload your Material's Texture")
        );
    }

    public override async Task HandleAsync(GetMaterialPostPresignedUrlRequest req, CancellationToken ct)
    {
        GetMaterialTexturePresignedUrlPostQuery presignedUrlQuery = new(
            MaterialName: req.MaterialName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetMaterialPostPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
