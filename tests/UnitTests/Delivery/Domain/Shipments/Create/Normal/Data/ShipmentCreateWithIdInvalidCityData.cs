namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal.Data;

using CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;
using static ShipmentsData;

public class ShipmentCreateWithIdInvalidCityData : ShipmentCreateData
{
	public ShipmentCreateWithIdInvalidCityData()
	{
		Add(ValidCountry1, InvalidCity, ValidReferenceId, ValidBuyerId);
	}
}
