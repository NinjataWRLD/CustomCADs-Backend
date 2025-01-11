namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Quantity.Increase.Data;

using static ActiveCartsData.CartItemsData;

public class IncreaseActiveCartItemQuantityValidData : IncreaseActiveCartItemQuantityData
{
    public IncreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
