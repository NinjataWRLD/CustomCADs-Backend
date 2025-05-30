using CustomCADs.Files.Application.Cads.Commands.Shared.Create;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create;

using Data;

public class CreateCadHandlerUnitTests : CadsBaseUnitTests
{
    private readonly CreateCadHandler handler;
    private readonly Mock<IWrites<Cad>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    public CreateCadHandlerUnitTests()
    {
        handler = new(writes.Object, uow.Object);
    }

    [Theory]
    [ClassData(typeof(CreateCadValidData))]
    public async Task Handle_ShouldPersistToDatabase(string key, string contentType, decimal volume)
    {
        // Arrange
        CreateCadCommand command = new(
            Key: key,
            ContentType: contentType,
            Volume: volume
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Cad>(x => x.Key == key && x.ContentType == contentType),
            ct
        ), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
