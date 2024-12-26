using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.GetItem;

public sealed record GetCartItemByIdQuery(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : IQuery<CartItemDto>;
