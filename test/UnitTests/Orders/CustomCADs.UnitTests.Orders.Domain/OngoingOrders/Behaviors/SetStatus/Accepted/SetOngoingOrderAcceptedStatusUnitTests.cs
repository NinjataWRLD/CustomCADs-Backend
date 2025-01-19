using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Accepted;

public class SetOngoingOrderAcceptedStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetAcceptedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        CreateOrder().SetAcceptedStatus();
    }

    [Fact]
    public void SetAcceptedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetAcceptedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetAcceptedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetAcceptedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetAcceptedStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetAcceptedStatus())
        );
    }
}
