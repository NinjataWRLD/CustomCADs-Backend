using CustomCADs.Files.Application.Images.IntegrationEventHandlers;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.UnitTests.Files.Application.Images.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IWrites<Image>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Image image = CreateImage();

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct)).ReturnsAsync(image);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenImageFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        writes.Verify(x => x.Remove(image), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenImageFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        storage.Verify(x => x.DeleteFileAsync(image.Key, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenImageNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(null as Image);

        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
