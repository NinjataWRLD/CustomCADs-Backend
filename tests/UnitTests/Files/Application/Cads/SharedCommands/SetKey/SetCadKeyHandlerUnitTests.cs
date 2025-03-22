using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey;

public class SetCadKeyHandlerUnitTests : CadsBaseUnitTests
{
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Cad cad = CreateCad();

    public SetCadKeyHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(cad);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldQueryDatabase(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldModifyCad_WhenCadFound(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(key, cad.Key);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldThrowException_WhenCadNotFound(string key)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct)).ReturnsAsync(null as Cad);

        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
