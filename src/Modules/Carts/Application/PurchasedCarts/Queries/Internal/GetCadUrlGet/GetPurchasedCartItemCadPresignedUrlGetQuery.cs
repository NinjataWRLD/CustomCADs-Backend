using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

public sealed record GetPurchasedCartItemCadPresignedUrlGetQuery(
	PurchasedCartId Id,
	ProductId ProductId,
	AccountId BuyerId
) : IQuery<GetPurchasedCartItemCadPresignedUrlGetDto>;
