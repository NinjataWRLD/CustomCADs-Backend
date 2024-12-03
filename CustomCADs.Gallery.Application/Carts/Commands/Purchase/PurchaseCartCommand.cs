using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Commands.Purchase;

public sealed record PurchaseCartCommand(
    string PaymentMethodId,
    CartId CartId,
    AccountId BuyerId
) : ICommand<string>;
