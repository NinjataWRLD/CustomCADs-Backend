﻿using CustomCADs.Orders.Application.CompletedOrders.Commands.Internal.Create;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Events;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Purchase.WithDelivery;

public sealed class PurchaseOngoingOrderWithDeliveryHandler(IOngoingOrderReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseOngoingOrderWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseOngoingOrderWithDeliveryCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.OrderId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.OrderId);

        if (order.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(order.Id);

        if (order.OrderStatus is not OngoingOrderStatus.Finished)
            throw CustomStatusException<OngoingOrder>.ById(order.Id, nameof(OngoingOrderStatus.Finished));

        if (!order.Delivery)
            throw CustomException.Delivery<OngoingOrder>(order.Delivery);

        if (order.DesignerId is null)
            throw CustomException.NullProp<OngoingOrder>(nameof(order.DesignerId));

        if (order.CadId is null)
            throw CustomException.NullProp<OngoingOrder>(nameof(order.CadId));

        if (order.Price is null)
            throw CustomException.NullProp<OngoingOrder>(nameof(order.Price));

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId),
            sellerQuery = new(order.DesignerId.Value);

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(buyerQuery, ct),
            sender.SendQueryAsync(sellerQuery, ct)
        ).ConfigureAwait(false);

        string buyer = users[0], seller = users[1];

        GetCustomizationCostByIdQuery costQuery = new(req.CustomizationId);
        decimal cost = await sender.SendQueryAsync(costQuery, ct).ConfigureAwait(false);

        decimal total =
            order.Price.Value * req.Count
            + cost * req.Count;

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: total,
            description: $"{buyer} bought {order.Name} from {seller} for {total}$.",
            ct
        ).ConfigureAwait(false);

        GetCustomizationWeightByIdQuery weightQuery = new(req.CustomizationId);
        double weight = await sender.SendQueryAsync(weightQuery, ct).ConfigureAwait(false);

        CreateCompletedOrderCommand command = new(
            Name: order.Name,
            Description: order.Description,
            Price: total,
            Delivery: true,
            OrderedAt: order.OrderedAt,
            BuyerId: order.BuyerId,
            DesignerId: order.DesignerId.Value,
            CadId: order.CadId.Value,
            CustomizationId: req.CustomizationId
        );
        CompletedOrderId completedOrderId = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new OngoingOrderDeliveryRequestedDomainEvent(
            Id: completedOrderId,
            ShipmentService: req.ShipmentService,
            Weight: weight * req.Count,
            Count: req.Count,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
