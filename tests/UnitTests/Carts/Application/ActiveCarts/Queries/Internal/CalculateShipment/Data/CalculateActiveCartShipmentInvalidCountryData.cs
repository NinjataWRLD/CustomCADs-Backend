using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.Data;

public class CalculateActiveCartShipmentInvalidCountryData : CalculateActiveCartShipmentData
{
	public CalculateActiveCartShipmentInvalidCountryData()
	{
		Add(new(null!, "Sofia", "Slivnitsa"));
		Add(new(string.Empty, "Bucharest", "Brailles"));
	}
}
