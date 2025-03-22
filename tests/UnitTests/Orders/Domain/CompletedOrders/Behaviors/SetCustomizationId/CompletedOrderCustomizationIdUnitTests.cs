using CustomCADs.Orders.Domain.CompletedOrders.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetCustomizationId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetCustomizationId;

public class CompletedOrderCustomizationIdUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderSetCustomizationIdValidData))]
    public void SetCustomizationId_ShouldNotThrowException_WhenOrderHasDelivery(CustomizationId customizationId)
    {
        CreateOrder(delivery: true).SetCustomizationId(customizationId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderSetCustomizationIdValidData))]
    public void SetCustomizationId_ShouldPopulateProperly(CustomizationId customizationId)
    {
        var order = CreateOrder(delivery: true);
        order.SetCustomizationId(customizationId);
        Assert.Equal(customizationId, order.CustomizationId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderSetCustomizationIdValidData))]
    public void SetCustomizationId_ShouldThrowException_WhenOrderDoesNotHasDelivery(CustomizationId customizationId)
    {
        Assert.Throws<CompletedOrderValidationException>(() =>
        {
            CreateOrder(delivery: false).SetCustomizationId(customizationId);
        });
    }
}
