﻿using CustomCADs.Files.Application.Images.Events.Application;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.ApplicationEvents.Files;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Files.Application.Images.Events.Application;

public class ProductDeletedHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IWrites<Image>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Image image = CreateImage();

    public ProductDeletedHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct)).ReturnsAsync(image);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenImageFound()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

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
        ProductDeletedApplicationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

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

        ProductDeletedApplicationEvent ie = new(
            Id: default,
            ImageId: id1,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Image>>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
