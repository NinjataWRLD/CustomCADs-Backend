using CustomCADs.Delivery.Application.Shipments.Commands.Edit;

namespace CustomCADs.Delivery.Endpoints.Shipments.Put;

public class PutShipmentEndpoint(IRequestSender sender)
    : Endpoint<PutShipmentRequest>
{
    public override void Configure()
    {
        Put("/{id}");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("02. Edit")
            .WithDescription("Edit your Shipment's Address by specifying its Id")
        );
    }

    public override async Task HandleAsync(PutShipmentRequest req, CancellationToken ct)
    {
        EditShipmentCommand command = new(
            Id: new ShipmentId(req.Id),
            Address: req.Address.ToAddress(),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct);

        await SendNoContentAsync();
    }
}
