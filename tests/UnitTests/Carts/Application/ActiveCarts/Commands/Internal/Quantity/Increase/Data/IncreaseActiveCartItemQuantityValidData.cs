namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increase.Data;

using static ActiveCartsData;

public class IncreaseActiveCartItemQuantityValidData : IncreaseActiveCartItemQuantityData
{
    public IncreaseActiveCartItemQuantityValidData()
    {
        Add(ValidQuantity1);
        Add(ValidQuantity2);
    }
}
