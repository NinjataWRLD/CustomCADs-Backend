namespace CustomCADs.Delivery.Endpoints.Shipments.Patch.Cancel;

public record CancelShipmentRequest(
    Guid Id,
    string Comment
);
