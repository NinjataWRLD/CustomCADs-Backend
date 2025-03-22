using CustomCADs.Catalog.Domain.Products.Exceptions;
using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

public class SetProductPriceUnitTests : ProductsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(SetProductPriceValidData))]
    public void SetPrice_ShouldNotThrow_WhenPriceIsValid(decimal price)
    {
        CreateProduct().SetPrice(price);
    }

    [Theory]
    [ClassData(typeof(SetProductPriceValidData))]
    public void SetPrice_ShouldPopulateProperly_WhenPriceIsValid(decimal price)
    {
        var product = CreateProduct();
        product.SetPrice(price);
        Assert.Equal(price, product.Price);
    }

    [Theory]
    [ClassData(typeof(SetProductPriceInvalidData))]
    public void SetPrice_ShouldThrowException_WhenPriceIsNotValid(decimal price)
    {
        Assert.Throws<ProductValidationException>(() =>
        {
            CreateProduct().SetPrice(price);
        });
    }
}
