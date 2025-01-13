namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.IncreaseQuantity.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemIncreaseQuantityInvalidData : ActiveCartItemIncreaseQuantityData
{
    public ActiveCartItemIncreaseQuantityInvalidData()
    {
        Add(InvalidQuantity);
    }
}
