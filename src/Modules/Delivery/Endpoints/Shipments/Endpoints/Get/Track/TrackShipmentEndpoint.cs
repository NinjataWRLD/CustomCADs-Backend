using CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Track;

public class TrackShipmentEndpoint(IRequestSender sender)
    : Endpoint<TrackShipmentRequest, Dictionary<string, TrackShipmentResponse>>
{
    public override void Configure()
    {
        Get("{id}/track");
        Group<ShipmentsGroup>();
        Description(d => d
            .WithSummary("02.Track")
            .WithDescription("See the tracking history of your shipment")
        );
    }

    public override async Task HandleAsync(TrackShipmentRequest req, CancellationToken ct)
    {
        GetShipmentTrackQuery query = new(
            Id: ShipmentId.New(req.Id)
        );
        Dictionary<DateTime, GetShipmentTrackDto> tracks = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = tracks.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
