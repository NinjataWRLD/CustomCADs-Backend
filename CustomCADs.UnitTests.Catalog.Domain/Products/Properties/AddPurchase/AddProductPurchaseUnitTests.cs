namespace CustomCADs.UnitTests.Catalog.Domain.Products.Properties.AddPurchase;

public class AddProductPurchaseUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Add_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product.AddToPurchaseCount();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
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
