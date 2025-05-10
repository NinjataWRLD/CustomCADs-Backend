namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;
using static ActiveCartsData;

public class CalculateActiveCartShipmentValidData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentValidData()
    {
        Add(ValidBuyerId, new("Bulgaria", "Sofia"));
        Add(ValidBuyerId, new("Romania", "Bucharest"));
    }
}
