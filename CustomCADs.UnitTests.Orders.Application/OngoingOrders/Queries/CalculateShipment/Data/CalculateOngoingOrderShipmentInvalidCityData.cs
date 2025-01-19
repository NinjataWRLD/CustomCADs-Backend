namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentInvalidCityData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentInvalidCityData()
    {
        Add("Bulgaria", null!);
        Add("Romania", string.Empty);
    }
}
