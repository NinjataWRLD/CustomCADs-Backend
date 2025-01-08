using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.UnitTests.Catalog.Domain.Products.Properties.SetName.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Properties.SetName;

public class SetProductNameUnitTests : ProductsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(SetProductNameValidData))]
    public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
    {
        CreateProduct().SetName(name);
    }

    [Theory]
    [ClassData(typeof(SetProductNameValidData))]
    public void SetName_ShouldPopulateProperly_WhenNameIsValid(string name)
    {
        var product = CreateProduct();
        product.SetName(name);
        Assert.Equal(name, product.Name);
    }

    [Theory]
    [ClassData(typeof(SetProductNameInvalidData))]
    public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
    {
        Assert.Throws<ProductValidationException>(() =>
        {
            CreateProduct().SetName(name);
        });
    }
}
