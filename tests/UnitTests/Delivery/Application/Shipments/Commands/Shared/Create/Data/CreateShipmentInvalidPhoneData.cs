namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidPhoneData : CreateShipmentData
{
    public CreateShipmentInvalidPhoneData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, InvalidPhone, ValidEmail1);
    }
}
