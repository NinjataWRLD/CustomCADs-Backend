using CustomCADs.Files.Application.Cads.SharedQueryHandlers;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedQueries.GetById;

public class GetCadByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();

    public GetCadByIdHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, track: false, ct).Returns(CreateCad());
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase()
    {
        // Assert
        GetCadByIdQuery query = new(id);
        GetCadByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, track: false, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss()
    {
        // Assert
        reads.SingleByIdAsync(id, false, ct).Returns(null as Cad);

        GetCadByIdQuery query = new(id);
        GetCadByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
