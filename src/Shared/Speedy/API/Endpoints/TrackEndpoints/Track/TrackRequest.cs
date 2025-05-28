namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints.Track;

using Dtos.ShipmentParcels;

public record TrackRequest(
	string UserName,
	string Password,
	TrackShipmentParcelRefDto[] Parcels,
	string? Language,
	long? ClientSystemId,
	bool? LastOperationOnly
);
