namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.ShipmentInfo;

using Dtos.Shipment;

internal record ShipmentInfoResponse(
	ShipmentDto[] Shipments,
	ErrorDto? Error
);
