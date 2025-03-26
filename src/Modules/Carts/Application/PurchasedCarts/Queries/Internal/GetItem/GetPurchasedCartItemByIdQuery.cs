using CustomCADs.Carts.Application.PurchasedCarts.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetItem;

public sealed record GetPurchasedCartItemByIdQuery(
    PurchasedCartId Id,
    ProductId ProductId,
    AccountId BuyerId
) : IQuery<PurchasedCartItemDto>;
