namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.WithId.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateWithIdValidData : ActiveCartItemCreateWithIdData
{
    public ActiveCartItemCreateWithIdValidData()
    {
        Add(ValidId1, ActiveCartsData.ValidId1, ValidProductId1, ValidWeight1, true);
        Add(ValidId2, ActiveCartsData.ValidId2, ValidProductId2, ValidWeight2, false);
    }
}
