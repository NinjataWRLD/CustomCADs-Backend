using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetReportedStatus;

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
