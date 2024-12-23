using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

public sealed record PurchaseOrderWithDeliveryCommand(
    string PaymentMethodId,
    double Weight,
    OrderId OrderId,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<string>;
