namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.CalculateShipment;

public record CalculateCustomShipmentRequest(
    Guid Id,
    int Count,
    string Country,
    string City,
    string Street,
    Guid CustomizationId
);
