using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Unchecked;

public class SetProductUncheckedStatusUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void SetUncheckedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        Assert.Multiple(
            () => CreateProduct().SetValidatedStatus().SetUncheckedStatus(),
            () => CreateProduct().SetReportedStatus().SetUncheckedStatus()
        );
    }

    [Fact]
    public void SetUncheckedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetUncheckedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetUncheckedStatus())
        );
    }
}
