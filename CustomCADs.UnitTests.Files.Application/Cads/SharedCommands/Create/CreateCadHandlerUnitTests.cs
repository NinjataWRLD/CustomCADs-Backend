using CustomCADs.Files.Application.Cads.SharedCommandHandlers.Create;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create;

public class CreateCadHandlerUnitTests : CadsBaseUnitTests
{
    private readonly IWrites<Cad> writes = Substitute.For<IWrites<Cad>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();

    [Theory]
    [ClassData(typeof(CreateCadValidData))]
    public async Task Handle_ShouldPersistToDatabase(string key, string contentType)
    {
        // Arrange
        CreateCadCommand command = new(
            Key: key,
            ContentType: contentType
        );
        CreateCadHandler handler = new(writes, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await writes.Received(1).AddAsync(
            Arg.Is<Cad>(x =>
                x.Key == key
                && x.ContentType == contentType
            ),
            ct: ct
        );
        await uow.Received(1).SaveChangesAsync(ct);
    }
}
