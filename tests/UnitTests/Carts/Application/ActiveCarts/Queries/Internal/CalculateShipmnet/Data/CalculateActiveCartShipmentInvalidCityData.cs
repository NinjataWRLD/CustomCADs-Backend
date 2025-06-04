namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

public class CalculateActiveCartShipmentInvalidCityData : CalculateActiveCartShipmentData
{
	public CalculateActiveCartShipmentInvalidCityData()
	{
		Add(new("Bulgaria", null!, "Slivnitsa"));
		Add(new("Romania", string.Empty, "Brailles"));
	}
}
