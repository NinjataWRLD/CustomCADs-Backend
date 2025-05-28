namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateInvalidCountryData : ShipmentCreateWithIdData
{
	public ShipmentCreateInvalidCountryData()
	{
		Add(InvalidCountry, ValidCity1, ValidReferenceId, ValidBuyerId);
	}
}
