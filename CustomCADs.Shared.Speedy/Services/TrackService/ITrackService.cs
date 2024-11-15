using CustomCADs.Shared.Speedy.Services.TrackService.BulkTrackingDataFiles;
using CustomCADs.Shared.Speedy.Services.TrackService.Track;
using Refit;

namespace CustomCADs.Shared.Speedy.Services.TrackService;

public interface ITrackService
{
    [Post("")]
    Task<TrackResponse> Track(TrackRequest request, CancellationToken ct = default);
    
    [Post("bulk")]
    Task<BulkTrackingDataFilesResponse> BulkTrackingDataFiles(BulkTrackingDataFilesRequest request, CancellationToken ct = default);
}
