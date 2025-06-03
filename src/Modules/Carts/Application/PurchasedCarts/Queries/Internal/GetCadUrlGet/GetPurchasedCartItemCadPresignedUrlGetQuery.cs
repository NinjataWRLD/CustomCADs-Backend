using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

public sealed record GetPurchasedCartItemCadPresignedUrlGetQuery(
	PurchasedCartId Id,
	ProductId ProductId,
	AccountId BuyerId
) : IQuery<GetPurchasedCartItemCadPresignedUrlGetDto>;
