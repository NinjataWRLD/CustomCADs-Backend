using CustomCADs.Catalog.Application.Products.Commands.AddLike;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.AddLike;

using static ProductsData;

public class AddProductLikeHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Product product = CreateProduct();

    public AddProductLikeHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenProductFound()
    {
        // Arrange
        AddProductLikeCommand command = new(ValidId);
        AddProductLikeHandler handler = new(reads.Object, uow.Object);

        // Act 
        //Assert
        await handler.Handle(command, ct);
    }
    
    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        AddProductLikeCommand command = new(ValidId);
        AddProductLikeHandler handler = new(reads.Object, uow.Object);

        // Act 
        await handler.Handle(command, ct);

        //Assert
        Assert.Equal(1, product.Counts.Likes);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        AddProductLikeCommand command = new(ProductId.New());
        AddProductLikeHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
