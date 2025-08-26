using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetCoords;

using static CadsData;

public class GetCadCoordsByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadCoordsByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly Cad cad = CreateCad();

	public GetCadCoordsByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCadCoordsByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadCoordsByIdQuery query = new(ValidId);

		// Act
		var (Cam, Pan) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(cad.CamCoordinates.X, Cam.X),
			() => Assert.Equal(cad.CamCoordinates.Y, Cam.Y),
			() => Assert.Equal(cad.CamCoordinates.Z, Cam.Z),
			() => Assert.Equal(cad.PanCoordinates.X, Pan.X),
			() => Assert.Equal(cad.PanCoordinates.Y, Pan.Y),
			() => Assert.Equal(cad.PanCoordinates.Z, Pan.Z)
		);
	}
}
