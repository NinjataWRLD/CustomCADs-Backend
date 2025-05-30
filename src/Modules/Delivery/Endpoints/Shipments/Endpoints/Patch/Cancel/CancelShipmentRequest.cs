namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Patch.Cancel;

public record CancelShipmentRequest(
	Guid Id,
	string Comment
);
