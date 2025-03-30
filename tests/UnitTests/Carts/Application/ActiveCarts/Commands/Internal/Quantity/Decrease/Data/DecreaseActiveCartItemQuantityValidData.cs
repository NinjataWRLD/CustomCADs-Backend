namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease.Data;

using static ActiveCartsData;

public class DecreaseActiveCartItemQuantityValidData : DecreaseActiveCartItemQuantityData
{
    public DecreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
