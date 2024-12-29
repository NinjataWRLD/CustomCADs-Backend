using CustomCADs.Files.Application.Images.SharedCommandHandlers.SetKey;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetKey.Data;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetKey;

using static ImagesData;

public class SetImageKeyHandlerData : TheoryData<string>;

public class SetImageKeyHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Image image = CreateImage();

    [Theory]
    [ClassData(typeof(SetImageKeyHandlerValidData))]
    public async Task Handle_ShouldCallDatabase(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(image);

        SetImageKeyCommand command = new(id, key);
        SetImageKeyHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [InlineData(ValidKey1)]
    [InlineData(ValidKey2)]
    public async Task Handle_ShouldModifyImage(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(image);

        SetImageKeyCommand command = new(id, key);
        SetImageKeyHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(key, image.Key);
    }
}
