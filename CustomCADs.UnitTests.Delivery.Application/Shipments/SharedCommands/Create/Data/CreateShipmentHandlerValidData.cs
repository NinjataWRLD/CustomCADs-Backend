namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentHandlerValidData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerValidData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
        Add(ValidService2, ValidCount2, ValidWeight2, ValidRecipient2, ValidCountry2, ValidCity2, ValidPhone2, ValidEmail2);
    }
}
