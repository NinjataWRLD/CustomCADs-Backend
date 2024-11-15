namespace CustomCADs.Shared.Speedy.Services.TrackService.Track;

using Dtos.Errors;
using Dtos.TrackedParcel;

public record TrackResponse(
    TrackedParcelDto[] Parcel,
    ErrorDto? Error
);
