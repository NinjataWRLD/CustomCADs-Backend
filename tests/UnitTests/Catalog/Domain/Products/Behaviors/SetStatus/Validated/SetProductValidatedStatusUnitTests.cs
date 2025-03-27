using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Validated;

public class SetProductValidatedStatusUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void SetValidatedStatus_ShouldNotThrowException_WhenStatusIsValid()
    {
        CreateProduct().SetValidatedStatus();
    }

    [Fact]
    public void SetValidatedStatus_ShouldThrowException_WhenStatusIsNotValid()
    {
        Assert.Multiple(
            () => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetValidatedStatus().SetValidatedStatus()),
            () => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetValidatedStatus()),
            () => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetValidatedStatus())
        );
    }
}
