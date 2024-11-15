namespace CustomCADs.Shared.Speedy.Services.TrackService.Track;

using Dtos.ShipmentParcels;

public record TrackRequest(
    string UserName,
    string Password,
    TrackShipmentParcelRefDto[] Parcels,
    string? Language,
    long? ClientSystemId,
    bool? LastOperationOnly
);
