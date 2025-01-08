namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.AddView;

public class AddProductViewUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Add_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product.AddToViewCount();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void Add_ShouldIncreaseViewCountProperly(int iterations)
    {
        var product = CreateProduct();

        for (int i = 0; i < iterations; i++)
        {
            product.AddToViewCount();
        }

        Assert.Equal(iterations, product.Counts.Views);
    }
}
