namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateWithIdInvalidCityData : ShipmentCreateWithIdData
{
	public ShipmentCreateWithIdInvalidCityData()
	{
		Add(ValidCountry1, InvalidCity, ValidReferenceId);
	}
}
