namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.CalculateShipment;

public record CalculateCustomShipmentRequest(
    Guid Id,
    int Count,
    string Country,
    string City,
    Guid CustomizationId
);
