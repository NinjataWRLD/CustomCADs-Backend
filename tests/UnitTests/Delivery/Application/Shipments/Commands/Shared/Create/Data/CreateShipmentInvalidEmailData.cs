namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidEmailData : CreateShipmentData
{
	public CreateShipmentInvalidEmailData()
	{
		Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, InvalidEmail);
	}
}
