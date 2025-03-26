using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentValidData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentValidData()
    {
        Add("Bulgaria", "Sofia");
        Add("Romania", "Bucharest");
    }
}
