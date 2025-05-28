namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.ShipmentInfo;

using Dtos.Shipment;

public record ShipmentInfoResponse(
	ShipmentDto[] Shipments,
	ErrorDto? Error
);
