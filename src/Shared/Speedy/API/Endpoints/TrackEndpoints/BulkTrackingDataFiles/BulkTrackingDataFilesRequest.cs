namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

public record BulkTrackingDataFilesRequest(
	string UserName,
	string Password,
	long? LastProcessedFileId,
	string? Language,
	long? ClientSystemId
);
