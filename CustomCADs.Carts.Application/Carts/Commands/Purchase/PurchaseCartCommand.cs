using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Commands.Purchase;

public sealed record PurchaseCartCommand(
    string PaymentMethodId,
    CartId CartId,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<string>;
