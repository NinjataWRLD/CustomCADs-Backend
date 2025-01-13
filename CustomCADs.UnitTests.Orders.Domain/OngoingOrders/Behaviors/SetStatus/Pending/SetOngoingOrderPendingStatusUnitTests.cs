using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Pending;

public class SetOngoingOrderPendingStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetPendingStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        Assert.Multiple(
            () => CreateOrder().SetAcceptedStatus().SetPendingStatus(),
            () => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetPendingStatus(),
            () => CreateOrder().SetReportedStatus().SetPendingStatus()
        );
    }

    [Fact]
    public void SetPendingStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetPendingStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetPendingStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetPendingStatus())
        );
    }
}
