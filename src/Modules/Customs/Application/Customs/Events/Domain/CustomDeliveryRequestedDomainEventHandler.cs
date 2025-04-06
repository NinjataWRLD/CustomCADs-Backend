using CustomCADs.Customs.Domain.Customs.Events;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Customs.Application.Customs.Events.Domain;

public class CustomDeliveryRequestedDomainEventHandler(ICustomReads reads, IRequestSender sender)
{
    public async Task Handle(CustomDeliveryRequestedDomainEvent de)
    {
        Custom custom = await reads.SingleByIdAsync(de.Id, track: false).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(de.Id);

        GetUsernameByIdQuery buyerQuery = new(custom.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery).ConfigureAwait(false);
        int count = de.Count;
        double weight = de.Weight;

        CreateShipmentCommand shipmentCommand = new(
            Info: new(count, weight, buyer),
            Service: de.ShipmentService,
            Address: de.Address,
            Contact: de.Contact,
            BuyerId: custom.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(shipmentCommand).ConfigureAwait(false);
        custom.SetShipment(shipmentId);
    }
}
