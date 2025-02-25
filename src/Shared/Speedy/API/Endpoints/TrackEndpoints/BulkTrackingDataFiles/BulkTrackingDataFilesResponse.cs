namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints.BulkTrackingDataFiles;

using Dtos.BulkTrackingDataFile;

public record BulkTrackingDataFilesResponse(
    BulkTrackingDataFileDto[] Parcels,
    ErrorDto? Error
);
