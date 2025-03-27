namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
    int Id,
    string ContentType,
    string FileName
);
