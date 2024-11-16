using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints;

using BulkTrackingDataFiles;
using Track;

public interface ITrackEndpoints
{
    [Post("")]
    Task<TrackResponse> Track(TrackRequest request, CancellationToken ct = default);

    [Post("bulk")]
    Task<BulkTrackingDataFilesResponse> BulkTrackingDataFiles(BulkTrackingDataFilesRequest request, CancellationToken ct = default);
}
