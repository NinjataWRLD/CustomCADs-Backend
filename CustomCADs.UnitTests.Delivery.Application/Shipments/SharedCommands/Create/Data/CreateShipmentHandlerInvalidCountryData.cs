namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentHandlerInvalidCountryData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerInvalidCountryData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, InvalidCountry, ValidCity1, ValidPhone1, ValidEmail1);
    }
}
