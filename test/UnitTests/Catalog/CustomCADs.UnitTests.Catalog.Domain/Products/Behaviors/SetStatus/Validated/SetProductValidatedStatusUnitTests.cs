﻿using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

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
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetValidatedStatus().SetValidatedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetValidatedStatus()),
            () => Assert.Throws<ProductValidationException>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetValidatedStatus())
        );
    }
}
