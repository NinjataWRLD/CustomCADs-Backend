namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentHandlerInvalidEmailData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerInvalidEmailData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, InvalidEmail);
    }
}
