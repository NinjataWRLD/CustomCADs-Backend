using CustomCADs.Shared.Core.Common.Exceptions.Domain;

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
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetFinishedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetFinishedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetFinishedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetFinishedStatus())
        );
    }
}
