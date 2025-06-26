using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

using Data;

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
	public void SetDescription_ShouldPopulateProperties_WhenDescriptionIsValid(string description)
	{
		var product = CreateProduct();
		product.SetDescription(description);
		Assert.Equal(description, product.Description);
	}

	[Theory]
	[ClassData(typeof(SetProductDescriptionInvalidData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionIsNotValid(string description)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetDescription(description)
		);
	}
}
