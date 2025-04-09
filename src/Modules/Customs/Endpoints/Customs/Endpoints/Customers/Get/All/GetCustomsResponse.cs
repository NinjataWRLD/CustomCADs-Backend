namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.All;

public sealed record GetCustomsResponse(
    Guid Id,
    string Name,
    DateTimeOffset OrderedAt,
    string Status,
    bool ForDelivery
);