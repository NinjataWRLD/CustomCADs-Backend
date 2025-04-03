namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.All;

public sealed record GetCustomsResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string BuyerName,
    bool ForDelivery
);