using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetContentType;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType;

public class SetCadContentTypeHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Cad cad = CreateCad();

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldQueryDatabase(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadContentTypeCommand command = new(id, contentType);
        SetCadContentTypeHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
    }
    
    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadContentTypeCommand command = new(id, contentType);
        SetCadContentTypeHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(SetCadContentTypeValidData))]
    public async Task Handle_ShouldModifyCad_WhenCadFound(string contentType)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadContentTypeCommand command = new(id, contentType);
        SetCadContentTypeHandler handler = new(reads, uow);

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
        reads.SingleByIdAsync(id, true, ct).Returns(null as Cad);

        SetCadContentTypeCommand command = new(id, contentType);
        SetCadContentTypeHandler handler = new(reads, uow);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
