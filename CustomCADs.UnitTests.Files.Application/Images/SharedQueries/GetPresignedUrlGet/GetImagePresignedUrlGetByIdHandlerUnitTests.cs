using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlGet;

public class GetImagePresignedUrlGetByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Image image = CreateImage();

    public GetImagePresignedUrlGetByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(image);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Assert
        GetImagePresignedUrlGetByIdQuery query = new(id1);
        GetImagePresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenImageFound()
    {
        // Assert
        GetImagePresignedUrlGetByIdQuery query = new(id1);
        GetImagePresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        storage.Verify(x => x.GetPresignedGetUrlAsync(
            image.Key,
            image.ContentType
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenImageNotFound()
    {
        // Assert
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(null as Image);

        GetImagePresignedUrlGetByIdQuery query = new(id1);
        GetImagePresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
