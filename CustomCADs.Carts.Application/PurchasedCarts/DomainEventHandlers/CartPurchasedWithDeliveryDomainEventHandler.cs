﻿using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Carts.Domain.PurchasedCarts.Events;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Carts.Application.PurchasedCarts.DomainEventHandlers;

public class CartPurchasedWithDeliveryDomainEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender)
{
    public async Task Handle(CartPurchasedWithDeliveryDomainEvent de)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(de.CartId, track: false).ConfigureAwait(false)
            ?? throw PurchasedCartNotFoundException.ById(de.CartId);

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
