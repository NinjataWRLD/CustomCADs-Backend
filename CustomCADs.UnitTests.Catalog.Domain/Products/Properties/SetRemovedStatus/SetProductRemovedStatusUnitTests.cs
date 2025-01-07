using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Properties.SetRemovedStatus;

public class SetProductRemovedStatusUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void SetRemovedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        CreateProduct().SetReportedStatus().SetRemovedStatus();
    }

    [Fact]
    public void SetRemovedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetRemovedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetValidatedStatus().SetRemovedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetRemovedStatus())
        );
    }
}
