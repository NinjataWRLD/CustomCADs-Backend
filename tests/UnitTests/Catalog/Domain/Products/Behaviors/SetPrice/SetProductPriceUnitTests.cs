using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

using Data;

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
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetPrice(price)
		);
	}
}
