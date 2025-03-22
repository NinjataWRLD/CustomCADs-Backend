using CustomCADs.Orders.Domain.OngoingOrders.Exceptions;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Removed;

public class SetOngoingOrderReportedStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetReportedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        Assert.Multiple(
            () => CreateOrder().SetReportedStatus(),
            () => CreateOrder().SetAcceptedStatus().SetReportedStatus(),
            () => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetReportedStatus(),
            () => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetFinishedStatus().SetReportedStatus()
        );
    }

    [Fact]
    public void SetReportedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetReportedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetReportedStatus())
        );
    }
}
