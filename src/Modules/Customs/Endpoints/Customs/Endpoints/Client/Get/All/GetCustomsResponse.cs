namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.All;

public sealed record GetCustomsResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string Status,
    bool ForDelivery
);