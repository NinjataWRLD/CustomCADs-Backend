using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;
using CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut;

public class GetImagePresignedUrlPutByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly IStorageService storage = Substitute.For<IStorageService>();
    private static readonly Image image = CreateImage();

    public GetImagePresignedUrlPutByIdHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false).Returns(image);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(string newContentType, string newFileName)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false);
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldCallStorage_WhenImageFound(string newContentType, string newFileName)
    {
        // Assert
        GetImagePresignedUrlPutByIdQuery query = new(
            id,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await storage.Received(1).GetPresignedPutUrlAsync(
            image.Key,
            newContentType,
            newFileName
        );
    }

    [Theory]
    [ClassData(typeof(GetImagePresignedUrlPutByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenImageNotFound(string newContentType, string newFileName)
    {
        // Assert
        reads.SingleByIdAsync(id, false).Returns(null as Image);

        GetImagePresignedUrlPutByIdQuery query = new(
            id,
            NewContentType: newContentType,
            NewFileName: newFileName
        );
        GetImagePresignedUrlPutByIdHandler handler = new(reads, storage);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
