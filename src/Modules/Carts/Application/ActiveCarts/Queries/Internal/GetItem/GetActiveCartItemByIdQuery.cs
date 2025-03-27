using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetItem;

public sealed record GetActiveCartItemByIdQuery(
    AccountId BuyerId,
    ProductId ProductId
) : IQuery<ActiveCartItemDto>;
