namespace CustomCADs.Shared.Speedy.Services.Track.Models;

public record TrackModel(
    TrackShipmentParcelRefModel[] Parcels,
    bool? LastOperationOnly
);
