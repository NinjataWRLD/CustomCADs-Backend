using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

public sealed record GetPurchasedCartByIdQuery(
	PurchasedCartId Id,
	AccountId BuyerId
) : IQuery<GetPurchasedCartByIdDto>;
