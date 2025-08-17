using CustomCADs.Speedy.Core.Services.Track.Models;

namespace CustomCADs.Speedy.Core.Contracts.Track;

internal interface ITrackService
{
	Task<(long Id, string Url)[]> BulkTrackingDataFiles(SpeedyAccount account, long? lastProcessedFileId = null, CancellationToken ct = default);
	Task<TrackedParcelModel[]> TrackAsync(SpeedyAccount account, SpeedyContact contact, string shipmentId, bool? lastOperationOnly = null, CancellationToken ct = default);
}
