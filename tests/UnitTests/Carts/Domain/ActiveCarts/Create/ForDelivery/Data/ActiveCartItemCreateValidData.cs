namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery.Data;

using static ActiveCartsData;

public class ActiveCartItemCreateValidData : ActiveCartItemCreateData
{
    public ActiveCartItemCreateValidData()
    {
        Add(ValidBuyerId1, ValidProductId1, ValidCustomizationId1);
        Add(ValidBuyerId2, ValidProductId2, ValidCustomizationId2);
    }
}
