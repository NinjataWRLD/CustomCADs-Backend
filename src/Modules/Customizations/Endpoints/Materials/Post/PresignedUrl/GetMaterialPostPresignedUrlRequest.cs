namespace CustomCADs.Customizations.Endpoints.Materials.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
    string MaterialName,
    string ContentType,
    string FileName
);
