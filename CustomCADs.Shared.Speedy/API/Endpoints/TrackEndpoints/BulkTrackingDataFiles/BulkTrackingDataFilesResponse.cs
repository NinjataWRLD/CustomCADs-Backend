namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;

public record BulkTrackingDataFilesResponse(
    BulkTrackingDataFileDto[] Parcel,
    ErrorDto? Error
);
