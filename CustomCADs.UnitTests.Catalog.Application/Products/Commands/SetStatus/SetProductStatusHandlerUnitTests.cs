using CustomCADs.Catalog.Application.Products.Commands.SetStatus;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.SetStatus;

using static ProductsData;

public class SetProductStatusHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Product product = CreateProduct();
    private const ProductStatus status = ProductStatus.Validated;

    public SetProductStatusHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        SetProductStatusCommand command = new(ValidId, status, ValidCreatorId);
        SetProductStatusHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersisttoDatabase()
    {
        // Arrange
        SetProductStatusCommand command = new(ValidId, status, ValidCreatorId);
        SetProductStatusHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        product.SetDesignerId(ValidDesignerId);

        SetProductStatusCommand command = new(ValidId, status, ValidCreatorId);
        SetProductStatusHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(null as Product);
        
        SetProductStatusCommand command = new(ValidId, status, ValidCreatorId);
        SetProductStatusHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
