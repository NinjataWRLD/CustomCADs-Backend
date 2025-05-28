namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.ShipmentId.Data;

using static PurchasedCartsData;

public class PurchasedCartSetShipmentIdValidData : PurchasedCartSetShipmentIdData
{
	public PurchasedCartSetShipmentIdValidData()
	{
		Add(
			new()
			{
				[CartItemsData.ValidProductId1] = CartItemsData.ValidPrice1,
				[CartItemsData.ValidProductId2] = CartItemsData.ValidPrice2
			},
			new()
			{
				[CartItemsData.ValidProductId1] = CartItemsData.ValidCadId1,
				[CartItemsData.ValidProductId2] = CartItemsData.ValidCadId2
			},
			new()
			{
				[CartItemsData.ValidCadId1] = CartItemsData.ValidCadId2,
				[CartItemsData.ValidCadId2] = CartItemsData.ValidCadId1
			}
		);
	}
}
