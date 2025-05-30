namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.FinalizePendingShipment;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record FinalizePendingShipmentResponse(
	// Copied from CreateShipmentResponse
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
