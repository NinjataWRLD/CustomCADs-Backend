namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.CalculateShipmnet.Data;

using static ActiveCartsData;

public class CalculateActiveCartShipmentValidData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentValidData()
    {
        Add(ValidBuyerId1, new("Bulgaria", "Sofia"));
        Add(ValidBuyerId2, new("Romania", "Bucharest"));
    }
}
