namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidWeightData : CreateShipmentData
{
    public CreateShipmentInvalidWeightData()
    {
        Add(ValidService1, ValidCount1, InvalidWeight1, ValidRecipient1, ValidCountry1, ValidCity1, ValidPhone1, ValidEmail1);
        Add(ValidService2, ValidCount2, InvalidWeight2, ValidRecipient2, ValidCountry2, ValidCity2, ValidPhone2, ValidEmail2);
    }
}
