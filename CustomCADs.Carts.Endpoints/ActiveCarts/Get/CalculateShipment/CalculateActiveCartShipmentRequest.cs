namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.CalculateShipment;

public record CalculateActiveCartShipmentRequest(
    Guid Id,
    string Country,
    string City
);
