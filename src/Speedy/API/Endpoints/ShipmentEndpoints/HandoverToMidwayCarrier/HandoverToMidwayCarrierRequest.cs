namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.HandoverToMidwayCarrier;

using Dtos.ShipmentParcels;

public record HandoverToMidwayCarrierRequest(
	string UserName,
	string Password,
	ParcelHandoverDto[] Parcels,
	string? Language,
	long? ClientSystemId
);
