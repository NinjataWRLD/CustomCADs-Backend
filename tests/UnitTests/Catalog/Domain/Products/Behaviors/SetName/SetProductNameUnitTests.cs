using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

using Data;

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
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetName(name)
		);
	}
}
