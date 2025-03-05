namespace CustomCADs.Customizations.Endpoints.Materials.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
    int Id,
    string ContentType,
    string FileName
);
