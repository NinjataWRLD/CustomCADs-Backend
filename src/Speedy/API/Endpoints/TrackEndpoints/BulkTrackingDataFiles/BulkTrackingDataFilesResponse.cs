namespace CustomCADs.Speedy.API.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;

public record BulkTrackingDataFilesResponse(
	BulkTrackingDataFileDto[] Parcels,
	ErrorDto? Error
);
