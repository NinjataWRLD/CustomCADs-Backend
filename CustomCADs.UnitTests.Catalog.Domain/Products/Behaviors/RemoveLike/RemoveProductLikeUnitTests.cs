using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.RemoveLike.Data;

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
    [ClassData(typeof(RemoveProductLikeValidData))]
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
    [ClassData(typeof(RemoveProductLikeValidData))]
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
