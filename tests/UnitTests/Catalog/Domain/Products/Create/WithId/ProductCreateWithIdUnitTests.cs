using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId;

using Data;
using static ProductsData;

public class ProductCreateWithIdUnitTests : ProductsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ProductCreateWithIdValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
	{
		CreateProductWithId(
			id: ValidId,
			name: name,
			description: description,
			price: price
		);
	}

	[Theory]
	[ClassData(typeof(ProductCreateWithIdValidData))]
	public void CreateWithId_ShouldPopulateProperties_WhenProductIsValid(string name, string description, decimal price)
	{
		Product product = CreateProductWithId(
			id: ValidId,
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
	[ClassData(typeof(ProductCreateWithIdInvalidNameData))]
	[ClassData(typeof(ProductCreateWithIdInvalidDescriptionData))]
	[ClassData(typeof(ProductCreateWithIdInvalidPriceData))]
	public void CreateWithId_ShouldThrowException_WhenProductIsNotValid(string name, string description, decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProductWithId(
				id: ValidId,
				name: name,
				description: description,
				price: price
			)
		);
	}
}
