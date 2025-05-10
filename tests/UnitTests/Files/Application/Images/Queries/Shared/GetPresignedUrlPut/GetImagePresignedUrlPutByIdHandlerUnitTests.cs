using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut;

using Data;

public class GetImagePresignedUrlPutByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly GetImagePresignedUrlPutByIdHandler handler;
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IStorageService> storage = new();

    private static readonly Image image = CreateImage();

    public GetImagePresignedUrlPutByIdHandlerUnitTests()
    {
        handler = new(reads.Object, storage.Object);
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct)).ReturnsAsync(image);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(UploadFileRequest newFile)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            newFile
        );

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldCallStorage_WhenImageFound(UploadFileRequest newFile)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            newFile
        );

        // Act
        await handler.Handle(query, ct);

        // Assert
        storage.Verify(x => x.GetPresignedPutUrlAsync(
            image.Key,
            newFile
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenImageNotFound(UploadFileRequest newFile)
    {
        // Assert
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct)).ReturnsAsync(null as Image);

        GetImagePresignedUrlPutByIdQuery query = new(
            id1,
            newFile
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Image>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
