using Refit;

namespace CustomCADs.Shared.Speedy.Services.TrackService;

using BulkTrackingDataFiles;
using Track;

public interface ITrackService
{
    [Post("")]
    Task<TrackResponse> Track(TrackRequest request, CancellationToken ct = default);

    [Post("bulk")]
    Task<BulkTrackingDataFilesResponse> BulkTrackingDataFiles(BulkTrackingDataFilesRequest request, CancellationToken ct = default);
}
