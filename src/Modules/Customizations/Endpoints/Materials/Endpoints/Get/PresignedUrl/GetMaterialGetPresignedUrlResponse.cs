namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.PresignedUrl;

public sealed record GetMaterialGetPresignedUrlResponse(
    string PresignedUrl,
    string ContentType
);
