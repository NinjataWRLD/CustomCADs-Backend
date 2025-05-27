namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateValidData : ShipmentCreateWithIdData
{
	public ShipmentCreateValidData()
	{
		Add(ValidCountry1, ValidCity1, ValidReferenceId, ValidBuyerId);
	}
}
