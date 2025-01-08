namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.AddLike;

public class AddProductLikeUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Add_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product.AddToLikeCount();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void Add_ShouldIncreaseLikeCountProperly(int iterations)
    {
        var product = CreateProduct();

        for (int i = 0; i < iterations; i++)
        {
            product.AddToLikeCount();
        }

        Assert.Equal(iterations, product.Counts.Likes);
    }
}
