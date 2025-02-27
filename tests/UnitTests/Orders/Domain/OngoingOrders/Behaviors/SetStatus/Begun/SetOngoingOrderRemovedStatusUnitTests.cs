using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Begun;

public class SetOngoingOrderRemovedStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetReportedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        CreateOrder().SetReportedStatus().SetRemovedStatus();
    }

    [Fact]
    public void SetReportedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetRemovedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetRemovedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetRemovedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetRemovedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetRemovedStatus())
        );
    }
}
