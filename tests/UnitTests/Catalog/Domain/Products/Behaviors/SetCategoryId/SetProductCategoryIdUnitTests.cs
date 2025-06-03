namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetCategoryId;

using static ProductsData;

public class SetProductCategoryIdUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void SetCategoryId_ShouldNotThrowException()
	{
		CreateProduct().SetCategoryId(ValidCategoryId);
	}

	[Fact]
	public void SetCategoryId_ShouldPopulateProperly()
	{
		var product = CreateProduct();
		product.SetCategoryId(ValidCategoryId);
		Assert.Equal(ValidCategoryId, product.CategoryId);
	}
}
