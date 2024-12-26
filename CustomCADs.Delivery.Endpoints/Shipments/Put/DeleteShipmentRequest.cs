namespace CustomCADs.Delivery.Endpoints.Shipments.Put;

public record PutShipmentRequest(Guid Id, AddressDto Address);
