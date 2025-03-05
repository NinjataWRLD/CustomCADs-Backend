namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.ForDelivery.Data;

using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.ForDelivery;
using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateValidData : ActiveCartItemCreateData
{
    public ActiveCartItemCreateValidData()
    {
        Add(ActiveCartsData.ValidId1, ValidProductId1, ValidCustomizationId1);
        Add(ActiveCartsData.ValidId2, ValidProductId2, ValidCustomizationId2);
    }
}
