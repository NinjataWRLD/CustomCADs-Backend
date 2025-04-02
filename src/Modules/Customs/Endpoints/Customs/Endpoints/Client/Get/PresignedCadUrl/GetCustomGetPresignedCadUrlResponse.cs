namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.PresignedCadUrl;

public sealed record GetCustomGetPresignedCadUrlResponse(
    string PresignedUrl,
    string ContentType
);
