using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Edit;

using static ProductsData;

public class EditProductHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Product product = CreateProduct();

    public EditProductHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldPersistToDatabase()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidDesignerId
        );
        EditProductHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(null as Product);

        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidDesignerId
        );
        EditProductHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
