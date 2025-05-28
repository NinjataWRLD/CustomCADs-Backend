using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetVolume;

public class GetCadVolumeByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly GetCadVolumeByIdHandler handler;
    private readonly Mock<ICadReads> reads = new();

    private static readonly Cad cad = CreateCad();

    public GetCadVolumeByIdHandlerUnitTests()
    {
        handler = new(reads.Object);
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(cad);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCadVolumeByIdQuery query = new(id1);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetCadVolumeByIdQuery query = new(id1);

        // Act
        decimal volume = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(cad.Volume, volume);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
            .ReturnsAsync(null as Cad);
        GetCadVolumeByIdQuery query = new(id1);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
            // Act
            async () => await handler.Handle(query, ct)
        );
    }
}
