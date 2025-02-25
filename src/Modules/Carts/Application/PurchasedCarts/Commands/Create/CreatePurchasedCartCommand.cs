using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;

public record CreatePurchasedCartCommand(
    AccountId BuyerId
) : ICommand<PurchasedCartId>;
