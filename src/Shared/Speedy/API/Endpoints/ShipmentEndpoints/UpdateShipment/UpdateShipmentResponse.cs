namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.UpdateShipment;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record UpdateShipmentResponse(
	// Copied from CreateShipmentResponse
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
