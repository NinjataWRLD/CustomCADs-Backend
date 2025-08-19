namespace CustomCADs.Speedy.Http.Endpoints.TrackEndpoints.Track;

using Dtos.TrackedParcel;

internal record TrackResponse(
	TrackedParcelDto[] Parcels,
	ErrorDto? Error
);
