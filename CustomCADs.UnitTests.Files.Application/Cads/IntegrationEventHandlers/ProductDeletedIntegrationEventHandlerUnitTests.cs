using CustomCADs.Files.Application.Cads.IntegrationEventHandlers;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.UnitTests.Files.Application.Cads.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandlerUnitTests : CadsBaseUnitTests
{
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IWrites<Cad>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Cad cad = CreateCad();

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct)).ReturnsAsync(cad);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            CadId: id1,
            ImageId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            CadId: id1,
            ImageId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        writes.Verify(x => x.Remove(cad), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenCadFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            CadId: id1,
            ImageId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        storage.Verify(x => x.DeleteFileAsync(cad.Key, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(null as Cad);

        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            CadId: id1,
            ImageId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
