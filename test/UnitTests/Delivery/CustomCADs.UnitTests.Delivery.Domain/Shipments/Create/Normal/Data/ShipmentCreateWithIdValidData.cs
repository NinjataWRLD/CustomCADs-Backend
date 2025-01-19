namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal.Data;

using CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;
using static ShipmentsData;

public class ShipmentCreateWithIdValidData : ShipmentCreateData
{
    public ShipmentCreateWithIdValidData()
    {
        Add(ValidCountry1, ValidCity1, ValidReferenceId, ValidBuyerId);
    }
}
