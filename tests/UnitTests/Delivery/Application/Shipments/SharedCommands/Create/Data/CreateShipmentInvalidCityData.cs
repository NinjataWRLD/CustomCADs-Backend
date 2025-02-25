namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidCityData : CreateShipmentData
{
    public CreateShipmentInvalidCityData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, InvalidCity, ValidPhone1, ValidEmail1);
    }
}
