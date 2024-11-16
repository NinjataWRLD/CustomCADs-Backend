namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints.Track;

using Dtos.TrackedParcel;

public record TrackResponse(
    TrackedParcelDto[] Parcel,
    ErrorDto? Error
);
