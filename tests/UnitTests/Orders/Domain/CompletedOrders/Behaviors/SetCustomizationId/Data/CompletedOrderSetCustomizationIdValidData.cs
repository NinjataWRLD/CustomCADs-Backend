namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetCustomizationId.Data;

using static CompletedOrdersData;

public class CompletedOrderSetCustomizationIdValidData : CompletedOrderSetCustomizationIdData
{
    public CompletedOrderSetCustomizationIdValidData()
    {
        Add(ValidCustomizationId1);
        Add(ValidCustomizationId2);
    }
}
