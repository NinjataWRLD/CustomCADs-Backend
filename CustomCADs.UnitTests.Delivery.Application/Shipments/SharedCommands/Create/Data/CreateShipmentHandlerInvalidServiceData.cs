namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentHandlerInvalidServiceData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerInvalidServiceData()
    {
        Add(InvalidService, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
    }
}
