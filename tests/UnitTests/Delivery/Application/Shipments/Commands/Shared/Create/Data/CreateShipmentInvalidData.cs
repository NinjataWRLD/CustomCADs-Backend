namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidData : TheoryData<string, int, double, string, string, string, string, string?, string?>
{
	public CreateShipmentInvalidData()
	{
		// Service
		Add(InvalidService, MinValidCount, MinValidWeight, ValidRecipient, ValidCountry, ValidCity, ValidStreet, ValidPhone, ValidEmail);

		// Info
		Add(ValidService, MinInvalidCount, MinValidWeight, ValidRecipient, ValidCountry, ValidCity, ValidStreet, ValidPhone, ValidEmail);
		Add(ValidService, MinValidCount, MinInvalidWeight, ValidRecipient, ValidCountry, ValidCity, ValidStreet, ValidPhone, ValidEmail);
		Add(ValidService, MinValidCount, MinValidWeight, InvalidRecipient, ValidCountry, ValidCity, ValidStreet, ValidPhone, ValidEmail);

		// Address
		Add(ValidService, MinValidCount, MinValidWeight, ValidRecipient, InvalidCountry, ValidCity, ValidStreet, ValidPhone, ValidEmail);
		Add(ValidService, MinValidCount, MinValidWeight, ValidRecipient, ValidCountry, InvalidCity, ValidStreet, ValidPhone, ValidEmail);
		Add(ValidService, MinValidCount, MinValidWeight, ValidRecipient, ValidCountry, ValidCity, InvalidStreet, ValidPhone, ValidEmail);

		// Contact
		Add(ValidService, MinValidCount, MinValidWeight, ValidRecipient, ValidCountry, ValidCity, ValidStreet, InvalidPhone, ValidEmail);
		Add(ValidService, MinValidCount, MinValidWeight, ValidRecipient, ValidCountry, ValidCity, ValidStreet, ValidPhone, InvalidEmail);
	}
}
