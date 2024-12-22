using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Purchase;

public sealed record PurchaseOrderCommand(
    string PaymentMethodId,
    OrderId OrderId,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<string>;
