namespace CustomCADs.Shared.Speedy.Services.TrackService.BulkTrackingDataFiles;

public record BulkTrackingDataFilesRequest(
    string UserName,
    string Password,
    long? LastProcessedFileId,
    string? Language,
    long? ClientSystemId
);
