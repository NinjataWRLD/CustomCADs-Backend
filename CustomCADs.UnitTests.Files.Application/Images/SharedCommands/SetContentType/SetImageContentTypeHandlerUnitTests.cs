using CustomCADs.Files.Application.Images.SharedCommandHandlers.SetContentType;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType;

public class SetImageContentTypeHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Image image = CreateImage();

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldQueryDatabase(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(image);

        SetImageContentTypeCommand command = new(id, contentType);
        SetImageContentTypeHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
    }
    
    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenImageFound(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(image);

        SetImageContentTypeCommand command = new(id, contentType);
        SetImageContentTypeHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public async Task Handle_ShouldModifyImage_WhenImageFound(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(image);

        SetImageContentTypeCommand command = new(id, contentType);
        SetImageContentTypeHandler handler = new(reads, uow);

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
        reads.SingleByIdAsync(id, true, ct).Returns(null as Image);

        SetImageContentTypeCommand command = new(id, contentType);
        SetImageContentTypeHandler handler = new(reads, uow);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
