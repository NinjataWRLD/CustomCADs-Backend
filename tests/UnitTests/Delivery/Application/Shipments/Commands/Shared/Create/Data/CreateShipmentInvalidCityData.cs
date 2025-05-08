namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidCityData : CreateShipmentData
{
    public CreateShipmentInvalidCityData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, ValidRecipient1, ValidCountry1, InvalidCity, ValidStreet1, ValidPhone1, ValidEmail1);
    }
}
