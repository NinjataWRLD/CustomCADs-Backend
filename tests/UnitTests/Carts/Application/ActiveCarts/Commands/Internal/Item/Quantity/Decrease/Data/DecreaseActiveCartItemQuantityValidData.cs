namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Quantity.Decrease.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Quantity.Decrease;
using static ActiveCartsData.CartItemsData;

public class DecreaseActiveCartItemQuantityValidData : DecreaseActiveCartItemQuantityData
{
    public DecreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
