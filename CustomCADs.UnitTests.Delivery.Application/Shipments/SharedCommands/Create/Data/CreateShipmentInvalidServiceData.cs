namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidServiceData : CreateShipmentData
{
    public CreateShipmentInvalidServiceData()
    {
        Add(InvalidService, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
    }
}
