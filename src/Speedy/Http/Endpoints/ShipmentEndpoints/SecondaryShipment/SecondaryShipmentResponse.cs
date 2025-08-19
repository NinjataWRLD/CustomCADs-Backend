namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.SecondaryShipment;

using Dtos.Shipment.Secondary;

internal record SecondaryShipmentResponse(
	SecondaryShipmentDto[] Shipments,
	ErrorDto? Error
);
