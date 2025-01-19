namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.CalculateShipmnet.Data;

using static ActiveCartsData;

public class CalculateActiveCartShipmentInvalidCityData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentInvalidCityData()
    {
        Add(ValidBuyerId1, new("Bulgaria", null!));
        Add(ValidBuyerId2, new("Romania", string.Empty));
    }
}
