namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateInvalidCityData : ShipmentCreateWithIdData
{
	public ShipmentCreateInvalidCityData()
	{
		Add(ValidCountry1, InvalidCity, ValidReferenceId, ValidBuyerId);
	}
}
