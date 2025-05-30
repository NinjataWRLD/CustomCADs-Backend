namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId.Data;

using static ShipmentsData;

public class ShipmentCreateWithIdValidData : ShipmentCreateWithIdData
{
    public ShipmentCreateWithIdValidData()
    {
        Add(ValidCountry1, ValidCity1, ValidReferenceId);
    }
}
