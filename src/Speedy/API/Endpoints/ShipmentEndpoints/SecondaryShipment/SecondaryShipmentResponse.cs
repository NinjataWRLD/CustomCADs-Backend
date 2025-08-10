namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.SecondaryShipment;

using Dtos.Shipment.Secondary;

public record SecondaryShipmentResponse(
	SecondaryShipmentDto[] Shipments,
	ErrorDto? Error
);
