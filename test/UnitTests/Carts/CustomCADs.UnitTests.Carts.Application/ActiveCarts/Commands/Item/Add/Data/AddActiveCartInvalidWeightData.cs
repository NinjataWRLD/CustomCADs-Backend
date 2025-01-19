namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add.Data;

using static ActiveCartsData;
using static ActiveCartsData.CartItemsData;

public class AddActiveCartInvalidWeightData : AddActiveCartData
{
    public AddActiveCartInvalidWeightData()
    {
        Add(ValidBuyerId1, InvalidWeight1, true, ValidProductId1);
        Add(ValidBuyerId2, InvalidWeight2, false, ValidProductId2);
    }
}
