namespace CustomCADs.Shared.Speedy.Services.TrackService.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;
using Dtos.Errors;

public record BulkTrackingDataFilesResponse(
    BulkTrackingDataFileDto[] Parcel,
    ErrorDto? Error
);
