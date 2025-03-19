using CustomCADs.Files.Application.Images.SharedCommandHandlers.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType;

public class SetImageContentTypeHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Image image = CreateImage();

    public SetImageContentTypeHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(image);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldQueryDatabase(string contentType)
    {
        // Arrange
        SetImageContentTypeCommand command = new(id1, contentType);
        SetImageContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenImageFound(string contentType)
    {
        // Arrange
        SetImageContentTypeCommand command = new(id1, contentType);
        SetImageContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldModifyImage_WhenImageFound(string contentType)
    {
        // Arrange
        SetImageContentTypeCommand command = new(id1, contentType);
        SetImageContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(contentType, image.ContentType);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldThrowException_WhenImageNotFound(string contentType)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(null as Image);

        SetImageContentTypeCommand command = new(id1, contentType);
        SetImageContentTypeHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
