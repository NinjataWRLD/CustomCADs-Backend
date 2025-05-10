namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

public class CalculateActiveCartShipmentInvalidCityData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentInvalidCityData()
    {
        Add(new("Bulgaria", null!));
        Add(new("Romania", string.Empty));
    }
}
