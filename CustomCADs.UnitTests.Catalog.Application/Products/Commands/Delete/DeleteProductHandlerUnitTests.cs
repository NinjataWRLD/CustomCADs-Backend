using CustomCADs.Catalog.Application.Products.Commands.Delete;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Delete;

using static ProductsData;

public class DeleteProductHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IWrites<Product>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Product product = CreateProduct();

    public DeleteProductHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        DeleteProductCommand command = new(ValidId, ValidCreatorId);
        DeleteProductHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }
    
    [Fact]
    public async Task Handler_ShouldPersistToDatabase()
    {
        // Arrange
        DeleteProductCommand command = new(ValidId, ValidCreatorId);
        DeleteProductHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.Remove(product), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldSendRequests()
    {
        // Arrange
        DeleteProductCommand command = new(ValidId, ValidCreatorId);
        DeleteProductHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseIntegrationEventAsync(It.IsAny<ProductDeletedIntegrationEvent>()), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        DeleteProductCommand command = new(ValidId, ValidDesignerId);
        DeleteProductHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

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

        DeleteProductCommand command = new(ValidId, ValidCreatorId);
        DeleteProductHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
