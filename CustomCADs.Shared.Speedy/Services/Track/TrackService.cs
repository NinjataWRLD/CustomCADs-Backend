using CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Services.Track.Models;

namespace CustomCADs.Shared.Speedy.Services.Track;

public class TrackService(ITrackEndpoints endpoints)
{
    public async Task<TrackedParcelModel[]> Track(TrackModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.Track(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. model.Parcels.Select(p => p.ToDto())],
            LastOperationOnly: model.LastOperationOnly
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Parcels.Select(p => p.ToModel())];
    }

    public async Task<(long Id, string Url)[]> BulkTrackingDataFiles(long? lastProcessedFileId, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.BulkTrackingDataFiles(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            LastProcessedFileId: lastProcessedFileId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Parcels.Select(p => (p.Id, p.Url))];
    }
}
