namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Data;

using static ShipmentsData;

public class ShipmentCreateInvalidCountryData : ShipmentCreateData
{
    public ShipmentCreateInvalidCountryData()
    {
        Add(InvalidCountry, ValidCity1, ValidReferenceId, ValidBuyerId);
    }
}
