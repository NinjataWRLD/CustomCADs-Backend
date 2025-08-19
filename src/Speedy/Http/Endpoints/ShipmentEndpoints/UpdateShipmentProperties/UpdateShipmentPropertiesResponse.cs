namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.UpdateShipmentProperties;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

internal record UpdateShipmentPropertiesResponse(
	// Copied from UpdateShipmentResponse
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
