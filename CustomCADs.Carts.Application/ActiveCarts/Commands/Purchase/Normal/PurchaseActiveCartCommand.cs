using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.Normal;

public sealed record PurchaseActiveCartCommand(
    string PaymentMethodId,
    AccountId BuyerId
) : ICommand<string>;
