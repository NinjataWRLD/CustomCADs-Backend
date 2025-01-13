namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.Normal.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateInvalidWeightData : ActiveCartItemCreateData
{
    public ActiveCartItemCreateInvalidWeightData()
    {
        Add(ActiveCartsData.ValidId1, ValidProductId1, InvalidWeight1, true);
        Add(ActiveCartsData.ValidId2, ValidProductId2, InvalidWeight2, false);
    }
}
