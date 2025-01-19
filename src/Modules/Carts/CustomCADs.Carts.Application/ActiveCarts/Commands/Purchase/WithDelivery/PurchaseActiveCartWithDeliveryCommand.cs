using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery;

public sealed record PurchaseActiveCartWithDeliveryCommand(
    string PaymentMethodId,
    string ShipmentService,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<string>;
