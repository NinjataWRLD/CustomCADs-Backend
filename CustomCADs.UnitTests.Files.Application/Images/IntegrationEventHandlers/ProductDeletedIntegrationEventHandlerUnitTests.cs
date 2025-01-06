using CustomCADs.Files.Application.Images.IntegrationEventHandlers;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.UnitTests.Files.Application.Images.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly IWrites<Image> writes = Substitute.For<IWrites<Image>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IStorageService storage = Substitute.For<IStorageService>();
    private static readonly Image image = CreateImage();

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, true, ct).Returns(image);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
    }
    
    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenImageFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        writes.Received(1).Remove(image);
        await uow.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenImageFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        await storage.Received(1).DeleteFileAsync(image.Key, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenImageNotFound()
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(null as Image);

        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: id,
            CadId: default
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
