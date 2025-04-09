namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;

public sealed record GetCustomResponse(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset OrderedAt,
    string Status,
    bool ForDelivery,
    string? DesignerName
);
