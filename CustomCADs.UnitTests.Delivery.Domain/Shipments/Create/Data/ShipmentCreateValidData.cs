namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Data;

public class ShipmentCreateValidData : ShipmentCreateData
{
    public ShipmentCreateValidData()
    {
        Add(ValidCountry1, ValidCity1, ValidReferenceId, ValidBuyerId);
    }
}
