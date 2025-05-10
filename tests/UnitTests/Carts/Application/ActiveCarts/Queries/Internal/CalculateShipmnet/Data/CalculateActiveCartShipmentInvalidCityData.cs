namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;
using static ActiveCartsData;

public class CalculateActiveCartShipmentInvalidCityData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentInvalidCityData()
    {
        Add(ValidBuyerId, new("Bulgaria", null!));
        Add(ValidBuyerId, new("Romania", string.Empty));
    }
}
