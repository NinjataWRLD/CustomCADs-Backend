namespace CustomCADs.Speedy.Http.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;

internal record BulkTrackingDataFilesResponse(
	BulkTrackingDataFileDto[] Parcels,
	ErrorDto? Error
);
