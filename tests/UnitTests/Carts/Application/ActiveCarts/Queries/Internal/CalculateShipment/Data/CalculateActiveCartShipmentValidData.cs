namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.Data;

public class CalculateActiveCartShipmentValidData : CalculateActiveCartShipmentData
{
	public CalculateActiveCartShipmentValidData()
	{
		Add(new("Bulgaria", "Sofia", "Slivnitsa"));
		Add(new("Romania", "Bucharest", "Brailles"));
	}
}
