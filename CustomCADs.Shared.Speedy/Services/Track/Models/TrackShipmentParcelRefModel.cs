using CustomCADs.Shared.Speedy.Models.Shipment.Parcel;

namespace CustomCADs.Shared.Speedy.Services.Track.Models;

public record TrackShipmentParcelRefModel(
    ShipmentParcelRefModel Parcel,
    string? Ref
);