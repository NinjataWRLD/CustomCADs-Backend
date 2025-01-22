namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;

public record GetActiveCartDto(
    ActiveCartId Id,
    string BuyerName,
    ICollection<ActiveCartItemDto> Items
);
