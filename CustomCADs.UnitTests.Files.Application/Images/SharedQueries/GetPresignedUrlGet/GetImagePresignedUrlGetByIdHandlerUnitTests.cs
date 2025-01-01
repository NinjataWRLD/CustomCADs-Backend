using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlGet;

public class GetImagePresignedUrlGetByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly IStorageService storage = Substitute.For<IStorageService>();
    private static readonly Image image = CreateImage();

    public GetImagePresignedUrlGetByIdHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false).Returns(image);
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase_WhenCadExists()
    {
        // Assert
        GetImagePresignedUrlGetByIdQuery query = new(id);
        GetImagePresignedUrlGetByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenCadExists()
    {
        // Assert
        GetImagePresignedUrlGetByIdQuery query = new(id);
        GetImagePresignedUrlGetByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await storage.Received(1).GetPresignedGetUrlAsync(
            image.Key,
            image.ContentType
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadDoesNotExists()
    {
        // Assert
        reads.SingleByIdAsync(id, false).Returns(null as Image);

        GetImagePresignedUrlGetByIdQuery query = new(id);
        GetImagePresignedUrlGetByIdHandler handler = new(reads, storage);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
