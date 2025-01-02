namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentHandlerInvalidRecipientData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerInvalidRecipientData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, InvalidRecipient, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
    }
}
