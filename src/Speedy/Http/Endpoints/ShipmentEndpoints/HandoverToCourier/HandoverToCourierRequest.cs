namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.HandoverToCourier;

using Dtos.ShipmentParcels;

internal record HandoverToCourierRequest(
	string UserName,
	string Password,
	ParcelHandoverDto[] Parcels,
	string? Language,
	long? ClientSystemId
);
