using CustomCADs.Carts.Application.ActiveCarts.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public record CreatePurchasedCartCommand(
    AccountId BuyerId,
    ActiveCartItemDto[] Items,
    Dictionary<ProductId, decimal> Prices
) : ICommand<PurchasedCartId>;
