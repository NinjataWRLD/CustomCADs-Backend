namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;

public sealed record DesignerGetCustomResponse(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset OrderedAt,
    string Status,
    bool ForDelivery,
    string BuyerName
);
