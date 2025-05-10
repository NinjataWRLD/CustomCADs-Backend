namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddView;

using Data;

public class AddProductViewUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Add_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product.AddToViewCount();
    }

    [Theory]
    [ClassData(typeof(AddProductViewValidData))]
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
