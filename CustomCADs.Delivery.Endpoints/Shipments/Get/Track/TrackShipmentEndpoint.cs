
using CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Track;

public class TrackShipmentEndpoint(IRequestSender sender)
    : Endpoint<TrackShipmentRequest, Dictionary<string, TrackShipmentResponse>>
{
    public override void Configure()
    {
        Get("track/{id}");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("02.Track")
            .WithDescription("See the tracking history of your shipment")
        );
    }

    public override async Task HandleAsync(TrackShipmentRequest req, CancellationToken ct)
    {
        GetShipmentTrackQuery query = new(
            Id: new ShipmentId(req.Id)
        );
        Dictionary<DateTime, GetShipmentTrackDto> tracks = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = tracks.ToTrackShipmentResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
