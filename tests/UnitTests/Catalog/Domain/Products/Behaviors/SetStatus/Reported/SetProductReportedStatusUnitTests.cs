using CustomCADs.Catalog.Domain.Products.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Reported;

public class SetProductReportedStatusUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void SetReportedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        Assert.Multiple(
            () => CreateProduct().SetReportedStatus(),
            () => CreateProduct().SetValidatedStatus().SetReportedStatus()
        );
    }

    [Fact]
    public void SetReportedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetReportedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetReportedStatus())
        );
    }
}
