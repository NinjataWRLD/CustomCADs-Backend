using CustomCADs.Shared.Core.Common.Exceptions.Domain;

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
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetPendingStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetPendingStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetPendingStatus())
        );
    }
}
