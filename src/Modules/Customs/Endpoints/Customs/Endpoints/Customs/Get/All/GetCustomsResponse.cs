namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.All;

public sealed record GetCustomsResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string Status,
    bool ForDelivery
);