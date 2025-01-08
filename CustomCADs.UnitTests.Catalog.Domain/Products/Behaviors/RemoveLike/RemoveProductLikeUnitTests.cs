using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.RemoveLike;

public class RemoveProductLikeUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void Remove_ShouldNotThrowException()
    {
        var product = CreateProduct();
        product
            .AddToLikeCount()
            .RemoveFromLikeCount();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void Add_ShouldDecreaseLikeCountProperly(int iterations)
    {
        var product = CreateProduct();

        for (int i = 0; i < iterations; i++)
        {
            product.AddToLikeCount();
        }
        for (int i = 0; i < iterations; i++)
        {
            product.RemoveFromLikeCount();
        }

        Assert.Equal(0, product.Counts.Likes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void Add_ShouldThrowException_WhenCountIsNotValid(int iterations)
    {
        var product = CreateProduct();

        for (int i = 0; i < iterations; i++)
        {
            product.AddToLikeCount();
        }
        for (int i = 0; i < iterations; i++)
        {
            product.RemoveFromLikeCount();
        }

        Assert.Throws<ProductValidationException>(() =>
        {
            product.RemoveFromLikeCount();
        });
    }
}
