using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetItem;

public sealed record GetPurchasedCartItemByIdQuery(
    PurchasedCartId Id,
    PurchasedCartItemId ItemId,
    AccountId BuyerId
) : IQuery<PurchasedCartItemDto>;
