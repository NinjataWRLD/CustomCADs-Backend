namespace CustomCADs.Shared.Speedy.Services.TrackService.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;

public record BulkTrackingDataFilesResponse(
    BulkTrackingDataFileDto[] Parcel,
    ErrorDto? Error
);
