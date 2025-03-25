using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

public class SetProductDescriptionUnitTests : ProductsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(SetProductDescriptionValidData))]
    public void SetDescription_ShouldNotThrow_WhenDescriptionIsValid(string description)
    {
        CreateProduct().SetDescription(description);
    }

    [Theory]
    [ClassData(typeof(SetProductDescriptionValidData))]
    public void SetDescription_ShouldPopulateProperly_WhenDescriptionIsValid(string description)
    {
        var product = CreateProduct();
        product.SetDescription(description);
        Assert.Equal(description, product.Description);
    }

    [Theory]
    [ClassData(typeof(SetProductDescriptionInvalidData))]
    public void SetDescription_ShouldThrowException_WhenDescriptionIsNotValid(string description)
    {
        Assert.Throws<CustomValidationException<Product>>(() =>
        {
            CreateProduct().SetDescription(description);
        });
    }
}
