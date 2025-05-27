using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId;

public class ProductCreateWithIdUnitTests : ProductsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ProductCreateWithIdValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenProductIsValid(ProductId id, string name, string description, decimal price)
	{
		CreateProductWithId(
			id: id,
			name: name,
			description: description,
			price: price
		);
	}

	[Theory]
	[ClassData(typeof(ProductCreateWithIdValidData))]
	public void CreateWithId_ShouldPopulateProperly_WhenProductIsValid(ProductId id, string name, string description, decimal price)
	{
		Product product = CreateProductWithId(
			id: id,
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
	public void CreateWithId_ShouldThrowException_WhenProductIsNotValid(ProductId id, string name, string description, decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(() =>
		{
			CreateProductWithId(
				id: id,
				name: name,
				description: description,
				price: price
			);
		});
	}
}
