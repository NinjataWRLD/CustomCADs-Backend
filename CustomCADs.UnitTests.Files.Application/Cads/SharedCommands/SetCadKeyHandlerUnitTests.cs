using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands;

public class SetCadKeyHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Cad cad = CreateCad();

    [Theory]
    [InlineData(ValidKey1)]
    [InlineData(ValidKey2)]
    public async Task Handle_ShouldCallDatabase(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadKeyCommand command = new(id, key);
        SetCadKeyHandler handler = new(reads, uow);

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
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadKeyCommand command = new(id, key);
        SetCadKeyHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(key, cad.Key);
    }
}
