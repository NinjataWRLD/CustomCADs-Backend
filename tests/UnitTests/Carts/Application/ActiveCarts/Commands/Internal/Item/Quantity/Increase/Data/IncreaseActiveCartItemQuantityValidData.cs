namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Quantity.Increase.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Quantity.Increase;
using static ActiveCartsData.CartItemsData;

public class IncreaseActiveCartItemQuantityValidData : IncreaseActiveCartItemQuantityData
{
    public IncreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
