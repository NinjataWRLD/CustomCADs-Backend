using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Endpoints.Shipments.Patch.Cancel;

public class CancelShipmentEndpoint(IRequestSender sender)
    : Endpoint<CancelShipmentRequest>
{
    public override void Configure()
    {
        Patch("{id}");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("03. Cancel")
            .WithDescription("Cancel a Shipment by providing its id (this doesn't delete the shipment, it just cancels the current delivery)")
        );
    }

    public override async Task HandleAsync(CancelShipmentRequest req, CancellationToken ct)
    {
        CancelShipmentCommand command = new(
            Id: ShipmentId.New(req.Id),
            Comment: req.Comment
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
