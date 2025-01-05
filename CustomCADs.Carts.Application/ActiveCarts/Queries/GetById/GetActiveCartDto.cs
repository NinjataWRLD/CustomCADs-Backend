using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetById;

public record GetActiveCartDto(
    ActiveCartId Id,
    AccountId BuyerId,
    ICollection<ActiveCartItemDto> Items
);
