namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.CalculateShipment;

public record CalculateCustomShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
