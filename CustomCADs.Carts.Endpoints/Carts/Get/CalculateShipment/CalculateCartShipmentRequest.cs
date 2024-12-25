namespace CustomCADs.Carts.Endpoints.Carts.Get.CalculateShipment;

public record CalculateCartShipmentRequest(
    Guid Id,
    string Country,
    string City
);
