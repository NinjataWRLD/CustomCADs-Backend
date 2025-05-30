namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.UpdateShipmentProperties;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record UpdateShipmentPropertiesResponse(
	// Copied from UpdateShipmentResponse
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
