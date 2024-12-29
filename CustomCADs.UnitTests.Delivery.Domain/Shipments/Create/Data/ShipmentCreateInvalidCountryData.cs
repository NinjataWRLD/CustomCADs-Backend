namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Data;

public class ShipmentCreateInvalidCountryData : ShipmentCreateData
{
    public ShipmentCreateInvalidCountryData()
    {
        Add(InvalidCountry, ValidCity1, ValidReferenceId, ValidBuyerId);
    }
}
