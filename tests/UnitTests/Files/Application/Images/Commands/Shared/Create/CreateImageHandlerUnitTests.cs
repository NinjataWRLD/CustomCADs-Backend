using CustomCADs.Files.Application.Images.Commands.Shared.Create;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create;

public class CreateImageHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IWrites<Image>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    [Theory]
    [ClassData(typeof(CreateImageValidData))]
    public async Task Handle_ShouldPersistToDatabase(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(
            Key: key,
            ContentType: contentType
        );
        CreateImageHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Image>(x =>
                x.Key == key
                && x.ContentType == contentType
            ),
        ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
