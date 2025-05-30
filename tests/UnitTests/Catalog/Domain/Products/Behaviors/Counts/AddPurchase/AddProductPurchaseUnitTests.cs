namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddPurchase;

using Data;

public class AddProductPurchaseUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Add_ShouldNotThrowException()
	{
		var product = CreateProduct();
		product.AddToPurchaseCount();
	}

	[Theory]
	[ClassData(typeof(AddProductPurchaseValidData))]
	public void Add_ShouldIncreasePurchaseCountProperly(int iterations)
	{
		var product = CreateProduct();

		for (int i = 0; i < iterations; i++)
		{
			product.AddToPurchaseCount();
		}

		Assert.Equal(iterations, product.Counts.Purchases);
	}
}
