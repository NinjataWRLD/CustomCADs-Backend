namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetForDelivery.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemSetForDeliveryValidData : ActiveCartItemSetForDeliveryData
{
    public ActiveCartItemSetForDeliveryValidData()
    {
        Add(ValidCustomizationId1);
        Add(ValidCustomizationId2);
    }
}
