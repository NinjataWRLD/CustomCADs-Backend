namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateWithIdInvalidCountryData : ShipmentCreateWithIdData
{
	public ShipmentCreateWithIdInvalidCountryData()
	{
		Add(InvalidCountry, ValidCity1, ValidReferenceId);
	}
}
