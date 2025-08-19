namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.CreateShipment;

using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

internal record CreateShipmentResponse(
	string Id,
	CreatedShipmentParcelDto[] Parcels,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
