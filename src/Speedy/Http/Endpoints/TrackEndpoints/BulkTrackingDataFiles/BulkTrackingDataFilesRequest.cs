namespace CustomCADs.Speedy.Http.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

internal record BulkTrackingDataFilesRequest(
	string UserName,
	string Password,
	long? LastProcessedFileId,
	string? Language,
	long? ClientSystemId
);
