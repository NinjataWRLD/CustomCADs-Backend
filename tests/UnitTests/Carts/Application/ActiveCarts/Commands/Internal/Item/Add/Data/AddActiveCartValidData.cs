namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Add.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Add;
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
