using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Shared.Create;

public sealed class CreateShipmentHandler(IWrites<Shipment> writes, IUnitOfWork uow, IDeliveryService delivery, IRequestSender sender)
    : ICommandHandler<CreateShipmentCommand, ShipmentId>
{
    public async Task<ShipmentId> Handle(CreateShipmentCommand req, CancellationToken ct)
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

        if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
            throw CustomNotFoundException<Shipment>.ById(req.BuyerId, "User");

        var shipment = Shipment.Create(
            address: new(req.Address.Country, req.Address.City),
            referenceId: reference.Id,
            buyerId: req.BuyerId
        );

        await writes.AddAsync(shipment, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return shipment.Id;
    }
}
