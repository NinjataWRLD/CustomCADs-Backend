using CustomCADs.Files.Application.Cads.SharedQueryHandlers;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedQueries.GetPresignedUrlGet;

public class GetCadPresignedUrlGetByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IStorageService> storage = new();
    private static readonly Cad cad = CreateCad();

    public GetCadPresignedUrlGetByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(cad);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Assert
        GetCadPresignedUrlGetByIdQuery query = new(id1);
        GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenCadFound()
    {
        // Assert
        GetCadPresignedUrlGetByIdQuery query = new(id1);
        GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        storage.Verify(x => x.GetPresignedGetUrlAsync(
            cad.Key,
            cad.ContentType
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadNotFound()
    {
        // Assert
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(null as Cad);

        GetCadPresignedUrlGetByIdQuery query = new(id1);
        GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
