namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidCountData : CreateShipmentData
{
    public CreateShipmentInvalidCountData()
    {
        Add(ValidService1, InvalidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
        Add(ValidService2, InvalidCount2, ValidWeight2, ValidRecipient2, ValidCountry2, ValidCity2, ValidPhone2, ValidEmail2);
    }
}
