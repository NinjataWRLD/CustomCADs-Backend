namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.FinalizePendingShipment;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

internal record FinalizePendingShipmentResponse(
	// Copied from CreateShipmentResponse
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
