using CustomCADs.Catalog.Application.Products.Commands.AddView;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.AddView;

using static ProductsData;

public class AddProductViewHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Product product = CreateProduct();

    public AddProductViewHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenProductFound()
    {
        // Arrange
        AddProductViewCommand command = new(ValidId);
        AddProductViewHandler handler = new(reads.Object, uow.Object);

        // Act 
        //Assert
        await handler.Handle(command, ct);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        AddProductViewCommand command = new(ValidId);
        AddProductViewHandler handler = new(reads.Object, uow.Object);

        // Act 
        await handler.Handle(command, ct);

        //Assert
        Assert.Equal(1, product.Counts.Views);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        AddProductViewCommand command = new(new ProductId());
        AddProductViewHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
