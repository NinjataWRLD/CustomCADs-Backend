using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddLike.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddLike;

public class AddProductLikeUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Add_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product.AddToLikeCount();
    }

    [Theory]
    [ClassData(typeof(AddProductLikeValidData))]
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
