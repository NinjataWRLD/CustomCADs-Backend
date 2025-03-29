namespace CustomCADs.Carts.Application.ActiveCarts;

internal static class Mapper
{
    internal static ActiveCartItemDto ToDto(this ActiveCartItem item, string buyer)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            BuyerName: buyer,
            AddedAt: item.AddedAt,
            BuyerId: item.BuyerId,
            ProductId: item.ProductId,
            CustomizationId: item.CustomizationId
        );
}
