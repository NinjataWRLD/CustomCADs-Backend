using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal;

using Data;

public class ProductCreateUnitTests : ProductsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ProductCreateValidData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
	{
		CreateProduct(
			name: name,
			description: description,
			price: price
		);
	}

	[Theory]
	[ClassData(typeof(ProductCreateValidData))]
	public void Create_ShouldPopulateProperties_WhenProductIsValid(string name, string description, decimal price)
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
	[ClassData(typeof(ProductCreateInvalidNameData))]
	[ClassData(typeof(ProductCreateInvalidDescriptionData))]
	[ClassData(typeof(ProductCreateInvalidPriceData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name, string description, decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct(
				name: name,
				description: description,
				price: price
			)
		);
	}
}
