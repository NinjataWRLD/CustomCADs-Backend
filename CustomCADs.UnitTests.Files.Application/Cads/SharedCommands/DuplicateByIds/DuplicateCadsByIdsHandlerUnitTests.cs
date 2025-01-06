using CustomCADs.Files.Application.Cads.SharedCommandHandlers.DuplicateByIds;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.DuplicateByIds;

using static CadsData;

public class DuplicateCadsByIdsHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IWrites<Cad> writes = Substitute.For<IWrites<Cad>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Cad[] cads = [
        CreateCadWithId(id1, ValidKey1, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1),
        CreateCadWithId(id2, ValidKey2, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2),
    ];
    private readonly CadId[] ids = [id1, id2];
    private readonly CadQuery query;
    private readonly Result<Cad> result;

    public DuplicateCadsByIdsHandlerUnitTests()
    {
        query = new(new(1, ids.Length), ids);
        result = new Result<Cad>(cads.Length, cads);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.AllAsync(query, false, ct).Returns(result);

        DuplicateCadsByIdsCommand command = new(ids);
        DuplicateCadsByIdsHandler handler = new(reads, writes, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).AllAsync(query, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        reads.AllAsync(query, false, ct).Returns(result);

        DuplicateCadsByIdsCommand command = new(ids);
        DuplicateCadsByIdsHandler handler = new(reads, writes, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await writes.Received(cads.Length).AddAsync(Arg.Any<Cad>(), ct);
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnKeysProperly()
    {
        // Arrange
        reads.AllAsync(query, false, ct).Returns(this.result);

        DuplicateCadsByIdsCommand command = new(ids);
        DuplicateCadsByIdsHandler handler = new(reads, writes, uow);

        // Act
        var result = await handler.Handle(command, ct);

        // Assert
        var actual = result.Select(x => x.Key);
        var expected = cads.Select(x => x.Id);
        Assert.Equal(expected, actual);
    }
}
