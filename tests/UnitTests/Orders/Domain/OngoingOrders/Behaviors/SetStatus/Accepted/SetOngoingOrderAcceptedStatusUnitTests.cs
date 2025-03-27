using CustomCADs.Shared.Core.Common.Exceptions.Domain;

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
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetAcceptedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetBegunStatus().SetAcceptedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetAcceptedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetAcceptedStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetAcceptedStatus())
        );
    }
}
