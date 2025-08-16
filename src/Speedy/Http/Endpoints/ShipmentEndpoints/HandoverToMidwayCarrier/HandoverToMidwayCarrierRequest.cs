namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.HandoverToMidwayCarrier;

using Dtos.ShipmentParcels;

internal record HandoverToMidwayCarrierRequest(
	string UserName,
	string Password,
	ParcelHandoverDto[] Parcels,
	string? Language,
	long? ClientSystemId
);
