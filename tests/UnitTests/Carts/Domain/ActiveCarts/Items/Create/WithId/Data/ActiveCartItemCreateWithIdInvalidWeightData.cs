namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.WithId.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateWithIdInvalidWeightData : ActiveCartItemCreateWithIdData
{
    public ActiveCartItemCreateWithIdInvalidWeightData()
    {
        Add(ValidId1, ActiveCartsData.ValidId1, ValidProductId1, InvalidWeight1, true);
        Add(ValidId2, ActiveCartsData.ValidId2, ValidProductId2, InvalidWeight2, false);
    }
}
