using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

public sealed record GetActiveCartItemQuery(
	AccountId BuyerId,
	ProductId ProductId
) : IQuery<ActiveCartItemDto>;
