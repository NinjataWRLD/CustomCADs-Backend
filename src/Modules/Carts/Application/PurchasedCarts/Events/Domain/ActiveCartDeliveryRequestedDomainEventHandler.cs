using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Carts.Application.PurchasedCarts.Events.Domain;

public class ActiveCartDeliveryRequestedDomainEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender)
{
    public async Task Handle(ActiveCartDeliveryRequestedDomainEvent de)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(de.Id, track: false).ConfigureAwait(false)
            ?? throw CustomNotFoundException<PurchasedCart>.ById(de.Id);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery).ConfigureAwait(false);
        double weight = de.Weight;
        int count = de.Count;

        CreateShipmentCommand command = new(
            Info: new(count, weight, buyer),
            Service: de.ShipmentService,
            Address: de.Address,
            Contact: de.Contact,
            BuyerId: cart.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(command).ConfigureAwait(false);
        cart.SetShipmentId(shipmentId);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
