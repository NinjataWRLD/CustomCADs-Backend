using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create;

public class CreateProductUnitTests : ProductsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public void Create_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
    {
        CreateProduct(
            name: name,
            description: description,
            price: price
        );
    }

    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public void Create_ShouldPopulateProperly_WhenProductIsValid(string name, string description, decimal price)
    {
        Product product = CreateProduct(
            name: name,
            description: description,
            price: price
        );

        Assert.Multiple(
            () => Assert.Equal(name, product.Name),
            () => Assert.Equal(description, product.Description),
            () => Assert.Equal(price, product.Price),
            () => Assert.Equal(ProductStatus.Unchecked, product.Status)
        );
    }

    [Theory]
    [ClassData(typeof(CreateProductInvalidNameData))]
    [ClassData(typeof(CreateProductInvalidDescriptionData))]
    [ClassData(typeof(CreateProductInvalidPriceData))]
    public void Create_ShouldThrowException_WhenProductIsNotValid(string name, string description, decimal price)
    {
        Assert.Throws<ProductValidationException>(() =>
        {
            CreateProduct(
                name: name,
                description: description,
                price: price
            );
        });
    }
}
