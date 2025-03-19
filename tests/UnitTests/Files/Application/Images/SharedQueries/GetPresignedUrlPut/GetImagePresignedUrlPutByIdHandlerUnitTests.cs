using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;
using CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut;

public class GetImagePresignedUrlPutByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Image image = CreateImage();

    public GetImagePresignedUrlPutByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct)).ReturnsAsync(image);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(string newContentType, string newFileName)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldCallStorage_WhenImageFound(string newContentType, string newFileName)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        storage.Verify(x => x.GetPresignedPutUrlAsync(
            image.Key,
            newContentType,
            newFileName
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenImageNotFound(string newContentType, string newFileName)
    {
        // Assert
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct)).ReturnsAsync(null as Image);

        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
