using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentInvalidCityData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentInvalidCityData()
    {
        Add("Bulgaria", null!);
        Add("Romania", string.Empty);
    }
}
