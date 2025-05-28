using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlGet;

public class GetCadPresignedUrlGetByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly GetCadPresignedUrlGetByIdHandler handler;
    private readonly Mock<ICadReads> reads = new();
    private readonly Mock<IStorageService> storage = new();

    public const string PresignedUrl = "Url";
    private static readonly Cad cad = CreateCad();

    public GetCadPresignedUrlGetByIdHandlerUnitTests()
    {
        handler = new(reads.Object, storage.Object);

        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(cad);

        storage.Setup(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType))
            .ReturnsAsync(PresignedUrl);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCadPresignedUrlGetByIdQuery query = new(id1);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallStorage()
    {
        // Arrange
        GetCadPresignedUrlGetByIdQuery query = new(id1);

        // Act
        await handler.Handle(query, ct);

        // Assert
        storage.Verify(x => x.GetPresignedGetUrlAsync(
            cad.Key,
            cad.ContentType
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetCadPresignedUrlGetByIdQuery query = new(id1);

        // Act
        var (Url, ContentType) = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(cad.ContentType, ContentType),
            () => Assert.Equal(PresignedUrl, Url)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(null as Cad);
        GetCadPresignedUrlGetByIdQuery query = new(id1);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
            // Act
            async () => await handler.Handle(query, ct)
        );
    }
}
