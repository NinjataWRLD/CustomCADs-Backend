﻿using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey;

public class SetCadKeyHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Cad cad = CreateCad();

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldQueryDatabase(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id1, true, ct).Returns(cad);

        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id1, true, ct);
    }
    
    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id1, true, ct).Returns(cad);

        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public async Task Handle_ShouldModifyCad_WhenCadFound(string key)
    {
        // Arrange
        reads.SingleByIdAsync(id1, true, ct).Returns(cad);

        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads, uow);

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
        reads.SingleByIdAsync(id1, true, ct).Returns(null as Cad);

        SetCadKeyCommand command = new(id1, key);
        SetCadKeyHandler handler = new(reads, uow);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
