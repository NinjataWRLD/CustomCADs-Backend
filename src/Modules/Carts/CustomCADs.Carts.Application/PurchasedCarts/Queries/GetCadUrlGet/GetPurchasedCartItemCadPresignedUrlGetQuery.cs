using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;

public sealed record GetPurchasedCartItemCadPresignedUrlGetQuery(
    PurchasedCartId Id,
    PurchasedCartItemId ItemId,
    AccountId BuyerId
) : IQuery<GetPurchasedCartItemCadPresignedUrlGetDto>;
