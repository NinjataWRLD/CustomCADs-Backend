using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public record CreatePurchasedCartCommand(
	AccountId BuyerId,
	ActiveCartItemDto[] Items,
	Dictionary<ProductId, decimal> Prices
) : ICommand<PurchasedCartId>;
