namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetForDelivery.Data;

using static ActiveCartsData;

public class ActiveCartItemSetForDeliveryValidData : ActiveCartItemSetForDeliveryData
{
	public ActiveCartItemSetForDeliveryValidData()
	{
		Add(ValidCustomizationId1);
		Add(ValidCustomizationId2);
	}
}
