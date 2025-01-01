using CustomCADs.Files.Application.Cads.SharedQueryHandlers;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedQueries.GetPresignedUrlGet;

public class GetCadPresignedUrlGetByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IStorageService storage = Substitute.For<IStorageService>();
    private static readonly Cad cad = CreateCad();

    public GetCadPresignedUrlGetByIdHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false).Returns(cad);
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase_WhenCadExists()
    {
        // Assert
        GetCadPresignedUrlGetByIdQuery query = new(id);
        GetCadPresignedUrlGetByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenCadExists()
    {
        // Assert
        GetCadPresignedUrlGetByIdQuery query = new(id);
        GetCadPresignedUrlGetByIdHandler handler = new(reads, storage);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await storage.Received(1).GetPresignedGetUrlAsync(
            cad.Key,
            cad.ContentType
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadDoesNotExists()
    {
        // Assert
        reads.SingleByIdAsync(id, false).Returns(null as Cad);

        GetCadPresignedUrlGetByIdQuery query = new(id);
        GetCadPresignedUrlGetByIdHandler handler = new(reads, storage);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
