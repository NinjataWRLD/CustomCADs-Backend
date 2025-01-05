using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;

public sealed record GetActiveCartItemByIdQuery(
    AccountId BuyerId,
    ActiveCartItemId ItemId
) : IQuery<ActiveCartItemDto>;
