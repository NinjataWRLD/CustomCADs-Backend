namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.CalculateShipmnet.Data;

using static ActiveCartsData;

public class CalculateActiveCartShipmentInvalidCountryData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentInvalidCountryData()
    {
        Add(ValidBuyerId1, new(null!, "Sofia"));
        Add(ValidBuyerId2, new(string.Empty, "Bucharest"));
    }
}
