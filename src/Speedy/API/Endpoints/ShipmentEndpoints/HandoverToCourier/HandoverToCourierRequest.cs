namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.HandoverToCourier;

using Dtos.ShipmentParcels;

public record HandoverToCourierRequest(
	string UserName,
	string Password,
	ParcelHandoverDto[] Parcels,
	string? Language,
	long? ClientSystemId
);
