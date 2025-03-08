namespace CustomCADs.Customizations.Endpoints.Materials.Get.PresignedUrl;

public sealed record GetMaterialGetPresignedUrlResponse(
    string PresignedUrl,
    string ContentType
);
