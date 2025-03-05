namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add.Data;

using static ActiveCartsData;
using static ActiveCartsData.CartItemsData;

public class AddActiveCartValidData : AddActiveCartData
{
    public AddActiveCartValidData()
    {
        Add(ValidBuyerId1, ValidCustomizationId1, true, ValidProductId1);
        Add(ValidBuyerId2, null, false, ValidProductId2);
    }
}
