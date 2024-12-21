using CustomCADs.Delivery.Application.Shipments.Commands.Delete;

namespace CustomCADs.Delivery.Endpoints.Shipments.Delete;

public class DeleteShipmentEndpoint(IRequestSender sender)
    : Endpoint<DeleteShipmentRequest>
{
    public override void Configure()
    {
        Delete("/{id}");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("03. Delete")
            .WithDescription("Delete your Shipment by specifying its Id")
        );
    }

    public override async Task HandleAsync(DeleteShipmentRequest req, CancellationToken ct)
    {
        DeleteShipmentCommand command = new(
            Id: new ShipmentId(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct);

        await SendNoContentAsync();
    }
}
