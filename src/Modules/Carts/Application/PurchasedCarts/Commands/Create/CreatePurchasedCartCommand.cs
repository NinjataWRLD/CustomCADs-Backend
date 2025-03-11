using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;

public record CreatePurchasedCartCommand(
    AccountId BuyerId,
    ActiveCartItemDto[] Items,
    Dictionary<ProductId, decimal> Prices
) : ICommand<PurchasedCartId>;
