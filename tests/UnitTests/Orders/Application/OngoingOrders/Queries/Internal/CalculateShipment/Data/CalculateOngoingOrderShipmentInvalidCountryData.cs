using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentInvalidCountryData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentInvalidCountryData()
    {
        Add(null!, "Sofia");
        Add(string.Empty, "Bucharest");
    }
}
