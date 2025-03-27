namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;

public record GetActiveCartDto(
    ActiveCartId Id,
    string BuyerName,
    ICollection<ActiveCartItemDto> Items
);
