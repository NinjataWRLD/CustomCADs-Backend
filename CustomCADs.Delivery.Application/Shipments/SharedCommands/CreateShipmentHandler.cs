using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.SharedCommands;

public sealed class CreateShipmentHandler(IWrites<Shipment> writes, IUnitOfWork uow)
    : ICommandHandler<CreateShipmentCommand, ShipmentId>
{
    public async Task<ShipmentId> Handle(CreateShipmentCommand req, CancellationToken ct)
    {
        Address address = new(req.Address.Country, req.Address.City);
        var shipment = Shipment.Create(address, req.BuyerId);

        await writes.AddAsync(shipment, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return shipment.Id;
    }
}
