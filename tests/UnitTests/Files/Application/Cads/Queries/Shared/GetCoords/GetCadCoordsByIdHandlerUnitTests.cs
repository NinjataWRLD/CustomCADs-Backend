using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetCoords;

using static CadsData;

public class GetCadCoordsByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadCoordsByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();

	private static readonly Cad cad = CreateCad();

	public GetCadCoordsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCadCoordsByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
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

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Cad);
		GetCadCoordsByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
