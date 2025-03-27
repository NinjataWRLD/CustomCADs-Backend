using CustomCADs.Shared.Core.Common.Exceptions.Domain;

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
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetBegunStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetBegunStatus().SetBegunStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetAcceptedStatus().SetFinishedStatus().SetBegunStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetBegunStatus()),
            () => Assert.Throws<CustomValidationException<OngoingOrder>>(() => CreateOrder().SetReportedStatus().SetRemovedStatus().SetBegunStatus())
        );
    }
}
