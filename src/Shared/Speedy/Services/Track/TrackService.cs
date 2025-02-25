using CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;
using CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Track.Models;

namespace CustomCADs.Shared.Speedy.Services.Track;

public class TrackService(
    ITrackEndpoints endpoints,
    ShipmentService shipmentService
)
{
    public async Task<TrackedParcelModel[]> TrackAsync(
        AccountModel account,
        string shipmentId,
        bool? lastOperationOnly = null,
        CancellationToken ct = default)
    {
        var shipments = await shipmentService.ShipmentInfoAsync(
            account: account,
            shipmentIds: [shipmentId],
            ct: ct
        ).ConfigureAwait(false);
        var parcels = shipments.Single().Content.Parcels;

        var response = await endpoints.Track(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels?.Select(p => new TrackShipmentParcelRefDto(null, Id: p.Id, null, null))],
            LastOperationOnly: lastOperationOnly
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Parcels.Select(p => p.ToModel())];
    }

    public async Task<(long Id, string Url)[]> BulkTrackingDataFiles(
        AccountModel account,
        long? lastProcessedFileId = null,
        CancellationToken ct = default)
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
