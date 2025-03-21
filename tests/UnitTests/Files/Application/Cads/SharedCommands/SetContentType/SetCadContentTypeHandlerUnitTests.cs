﻿using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetContentType;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType;

public class SetCadContentTypeHandlerUnitTests : CadsBaseUnitTests
{
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Cad cad = CreateCad();

    public SetCadContentTypeHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(cad);
    }

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldQueryDatabase(string contentType)
    {
        // Arrange
        SetCadContentTypeCommand command = new(id1, contentType);
        SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound(string contentType)
    {
        // Arrange
        SetCadContentTypeCommand command = new(id1, contentType);
        SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldModifyCad_WhenCadFound(string contentType)
    {
        // Arrange
        SetCadContentTypeCommand command = new(id1, contentType);
        SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(contentType, cad.ContentType);
    }

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldThrowException_WhenCadNotFound(string contentType)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
            .ReturnsAsync(null as Cad);

        SetCadContentTypeCommand command = new(id1, contentType);
        SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
