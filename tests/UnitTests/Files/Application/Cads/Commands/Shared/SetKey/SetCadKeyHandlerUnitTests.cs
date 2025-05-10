using CustomCADs.Files.Application.Cads.Commands.Shared.SetKey;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey;

using Data;

public class SetCadKeyHandlerUnitTests : CadsBaseUnitTests
{
    private readonly SetCadKeyHandler handler;
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private readonly Cad cad = CreateCad();

    public SetCadKeyHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object);
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(cad);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldQueryDatabase(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id1, key);

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

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
