using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Track;

public class TrackShipmentEndpoint(IRequestSender sender)
    : Endpoint<TrackShipmentRequest, Dictionary<string, TrackShipmentResponse>>
{
    public override void Configure()
    {
        Get("{id}/track");
        Group<ShipmentsGroup>();
        Description(d => d
            .WithSummary("Track")
            .WithDescription("See the tracking history of your shipment")
        );
    }

    public override async Task HandleAsync(TrackShipmentRequest req, CancellationToken ct)
    {
        Dictionary<DateTime, GetShipmentTrackDto> tracks = await sender.SendQueryAsync(
            new GetShipmentTrackQuery(
                Id: ShipmentId.New(req.Id)
            ),
            ct
        ).ConfigureAwait(false);

        var response = tracks.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
