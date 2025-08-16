using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Track.Models;

namespace CustomCADs.Speedy.Core.Contracts.Track;

internal interface ITrackService
{
	Task<(long Id, string Url)[]> BulkTrackingDataFiles(AccountModel account, long? lastProcessedFileId = null, CancellationToken ct = default);
	Task<TrackedParcelModel[]> TrackAsync(AccountModel account, string shipmentId, bool? lastOperationOnly = null, CancellationToken ct = default);
}
