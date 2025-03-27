using CustomCADs.Shared.Core.Common.Exceptions.Domain;

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
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetRemovedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetRemovedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetRemovedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetRemovedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetRemovedStatus())
        );
    }
}
