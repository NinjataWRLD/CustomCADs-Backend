using CustomCADs.Files.Application.Images.SharedCommandHandlers.Create;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands;

public class CreateImageHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IWrites<Image> writes = Substitute.For<IWrites<Image>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();

    [Theory]
    [InlineData(ValidKey1, ValidContentType1)]
    [InlineData(ValidKey2, ValidContentType2)]
    public async Task Handle_ShouldCallDatabase(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(
            Key: key,
            ContentType: contentType
        );
        CreateImageHandler handler = new(writes, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await writes.Received(1).AddAsync(
            Arg.Is<Image>(x =>
                x.Key == key
                && x.ContentType == contentType
            ),
            ct: ct
        );
        await uow.Received(1).SaveChangesAsync(ct);
    }
}
