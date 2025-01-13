namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Quantity.Decrease.Data;

using static ActiveCartsData.CartItemsData;

public class DecreaseActiveCartItemQuantityValidData : DecreaseActiveCartItemQuantityData
{
    public DecreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
