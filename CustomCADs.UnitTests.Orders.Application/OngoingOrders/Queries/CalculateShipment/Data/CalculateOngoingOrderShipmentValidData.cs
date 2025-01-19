namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.CalculateShipment.Data;

public class CalculateOngoingOrderShipmentValidData : CalculateOngoingOrderShipmentData
{
    public CalculateOngoingOrderShipmentValidData()
    {
        Add("Bulgaria", "Sofia");
        Add("Romania", "Bucharest");
    }
}
