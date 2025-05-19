namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidCountryData : CreateShipmentData
{
	public CreateShipmentInvalidCountryData()
	{
		Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, InvalidCountry, ValidCity1, ValidPhone1, ValidEmail1);
	}
}
