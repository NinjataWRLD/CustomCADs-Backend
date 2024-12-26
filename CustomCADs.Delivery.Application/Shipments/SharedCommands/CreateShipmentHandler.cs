using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.SharedCommands;

public sealed class CreateShipmentHandler(IWrites<Shipment> writes, IUnitOfWork uow, IDeliveryService delivery)
    : ICommandHandler<CreateShipmentCommand, (ShipmentId Id, decimal Price)>
{
    public async Task<(ShipmentId Id, decimal Price)> Handle(CreateShipmentCommand req, CancellationToken ct)
    {
        ShipRequestDto request = new(
            Package: "BOX",
            Contents: $"{req.Info.Count} 3D Model/s, each wrapped in a box",
            ParcelCount: req.Info.Count,
            Name: req.Info.Recipient,
            TotalWeight: req.Info.Weight,
            Service: req.Service,
            Country: req.Address.Country,
            City: req.Address.City,
            Phone: req.Contact.Phone,
            Email: req.Contact.Email
        );
        ShipmentDto reference = await delivery.ShipAsync(request, ct).ConfigureAwait(false);

        var shipment = Shipment.Create(
            address: new(req.Address.Country, req.Address.City),
            referenceId: reference.Id,
            buyerId: req.BuyerId
        );

        await writes.AddAsync(shipment, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return (shipment.Id, reference.Price);
    }
}
