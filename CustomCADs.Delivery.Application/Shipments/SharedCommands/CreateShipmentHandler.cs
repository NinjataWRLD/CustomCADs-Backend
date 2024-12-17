using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.SharedCommands;

public sealed class CreateShipmentHandler(IWrites<Shipment> writes, IUnitOfWork uow)
    : ICommandHandler<CreateShipmentCommand, ShipmentId>
{
    public async Task<ShipmentId> Handle(CreateShipmentCommand req, CancellationToken ct)
    {
        var shipment = Shipment.Create(new(), req.BuyerId); // populate address

        await writes.AddAsync(shipment, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return shipment.Id;
    }
}
