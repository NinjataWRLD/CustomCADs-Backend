using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Endpoints.Shipments.Patch.Cancel;

public class CancelShipmentEndpoint(IRequestSender sender)
    : Endpoint<CancelShipmentRequest>
{
    public override void Configure()
    {
        Patch("");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("03. Cancel")
            .WithDescription("Cancel a Shipment (this doesn't delete the shipment, it simply cancels the requested delivery)")
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
