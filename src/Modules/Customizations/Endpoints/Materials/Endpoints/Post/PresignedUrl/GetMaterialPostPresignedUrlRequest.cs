namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
    string MaterialName,
    string ContentType,
    string FileName
);
