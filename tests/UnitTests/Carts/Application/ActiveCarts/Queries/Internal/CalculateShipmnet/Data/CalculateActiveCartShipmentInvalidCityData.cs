namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;
using static ActiveCartsData;

public class CalculateActiveCartShipmentInvalidCityData : CalculateActiveCartShipmentData
{
	public CalculateActiveCartShipmentInvalidCityData()
	{
		Add(ValidBuyerId1, new("Bulgaria", null!));
		Add(ValidBuyerId2, new("Romania", string.Empty));
	}
}
