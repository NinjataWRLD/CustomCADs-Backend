namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add.Data;

using static ActiveCartsData;

public class AddActiveCartValidData : AddActiveCartData
{
    public AddActiveCartValidData()
    {
        Add(ValidBuyerId, ValidCustomizationId, true, ValidProductId);
        Add(ValidBuyerId, null, false, ValidProductId);
    }
}
