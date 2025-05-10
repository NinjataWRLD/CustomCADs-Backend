namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

public class CalculateActiveCartShipmentInvalidCountryData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentInvalidCountryData()
    {
        Add(new(null!, "Sofia"));
        Add(new(string.Empty, "Bucharest"));
    }
}
