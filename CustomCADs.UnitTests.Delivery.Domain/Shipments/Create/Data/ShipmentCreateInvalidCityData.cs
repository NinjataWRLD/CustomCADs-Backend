namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Data;

using static ShipmentsData;

public class ShipmentCreateInvalidCityData : ShipmentCreateData
{
    public ShipmentCreateInvalidCityData()
    {
        Add(ValidCountry1, InvalidCity, ValidReferenceId, ValidBuyerId);
    }
}
