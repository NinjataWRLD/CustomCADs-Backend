namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;
using static ActiveCartsData;

public class CalculateActiveCartShipmentValidData : CalculateActiveCartShipmentData
{
    public CalculateActiveCartShipmentValidData()
    {
        Add(ValidBuyerId1, new("Bulgaria", "Sofia", "Flora"));
        Add(ValidBuyerId2, new("Romania", "Bucharest", "Brailles"));
    }
}
