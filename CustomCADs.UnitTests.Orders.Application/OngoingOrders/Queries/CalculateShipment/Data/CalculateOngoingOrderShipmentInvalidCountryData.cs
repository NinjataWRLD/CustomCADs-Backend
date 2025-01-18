namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentInvalidCountryData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentInvalidCountryData()
    {
        Add(null!, "Sofia");
        Add(string.Empty, "Bucharest");
    }
}
