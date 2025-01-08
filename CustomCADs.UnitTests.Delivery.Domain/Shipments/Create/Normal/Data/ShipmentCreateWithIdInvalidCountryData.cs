namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal.Data;

using CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;
using static ShipmentsData;

public class ShipmentCreateWithIdInvalidCountryData : ShipmentCreateData
{
    public ShipmentCreateWithIdInvalidCountryData()
    {
        Add(InvalidCountry, ValidCity1, ValidReferenceId, ValidBuyerId);
    }
}
