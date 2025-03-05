using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

public sealed record PurchaseOngoingOrderWithDeliveryCommand(
    OngoingOrderId OrderId,
    string PaymentMethodId,
    string ShipmentService,
    int Count,
    AddressDto Address,
    ContactDto Contact,
    CustomizationId CustomizationId,
    AccountId BuyerId
) : ICommand<string>;
