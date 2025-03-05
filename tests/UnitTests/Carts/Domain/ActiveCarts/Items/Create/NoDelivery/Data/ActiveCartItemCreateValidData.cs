namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.NoDelivery.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateValidData : ActiveCartItemCreateData
{
    public ActiveCartItemCreateValidData()
    {
        Add(ActiveCartsData.ValidId1, ValidProductId1);
        Add(ActiveCartsData.ValidId2, ValidProductId2);
    }
}
