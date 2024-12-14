using CustomCADs.Carts.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.GetItems;

public sealed record GetCartItemsByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<ICollection<CartItemDto>>;
