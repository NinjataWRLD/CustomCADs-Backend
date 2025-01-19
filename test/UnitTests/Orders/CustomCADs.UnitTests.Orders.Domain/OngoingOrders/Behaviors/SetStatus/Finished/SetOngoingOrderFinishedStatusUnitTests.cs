using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Finished;

public class SetOngoingOrderFinishedStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetFinishedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        Assert.Multiple(
            () => CreateOrder().SetAcceptedStatus().SetFinishedStatus(),
            () => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetFinishedStatus()
        );
    }

    [Fact]
    public void SetFinishedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetFinishedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetFinishedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetFinishedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetFinishedStatus())
        );
    }
}
