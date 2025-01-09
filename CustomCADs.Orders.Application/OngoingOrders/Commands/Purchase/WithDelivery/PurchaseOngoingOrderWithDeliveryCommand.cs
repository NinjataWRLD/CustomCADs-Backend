using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

public sealed record PurchaseOngoingOrderWithDeliveryCommand(
    OngoingOrderId OrderId,
    string PaymentMethodId,
    string ShipmentService,
    double Weight,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<string>;
