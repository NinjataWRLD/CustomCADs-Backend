using CustomCADs.Orders.Domain.OngoingOrders.Exceptions;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetStatus.Reported;

public class SetOngoingOrderBegunStatusUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetBegunStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        CreateOrder().SetAcceptedStatus().SetBegunStatus();
    }

    [Fact]
    public void SetBegunStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetBegunStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetBegunStatus().SetBegunStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetBegunStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetBegunStatus()),
            () => Assert.Throws<OngoingOrderValidationException>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetBegunStatus())
        );
    }
}
