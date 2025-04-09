namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;

public sealed record GetCustomResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string Status,
    bool ForDelivery,
    string? DesignerName
);
