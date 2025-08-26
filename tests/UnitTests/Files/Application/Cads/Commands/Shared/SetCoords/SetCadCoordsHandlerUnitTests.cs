using CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords;

using static CadsData;

public class SetCadCoordsHandlerUnitTests : CadsBaseUnitTests
{
	private readonly SetCadCoordsHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly CoordinatesDto camCoords = new(MinValidCoord, MinValidCoord, MinValidCoord);
	private static readonly CoordinatesDto panCoords = new(MaxValidCoord, MaxValidCoord, MaxValidCoord);
	private readonly Cad cad = CreateCadWithId();

	public SetCadCoordsHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: ValidId,
			CamCoordinates: camCoords,
			PanCoordinates: panCoords
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: ValidId,
			CamCoordinates: camCoords,
			PanCoordinates: panCoords
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: ValidId,
			CamCoordinates: camCoords,
			PanCoordinates: panCoords
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, cad),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldModifyCad()
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: ValidId,
			CamCoordinates: camCoords,
			PanCoordinates: panCoords
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(camCoords.X, cad.CamCoordinates.X),
			() => Assert.Equal(camCoords.Y, cad.CamCoordinates.Y),
			() => Assert.Equal(camCoords.Z, cad.CamCoordinates.Z),
			() => Assert.Equal(panCoords.X, cad.PanCoordinates.X),
			() => Assert.Equal(panCoords.Y, cad.PanCoordinates.Y),
			() => Assert.Equal(panCoords.Z, cad.PanCoordinates.Z)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Cad);

		SetCadCoordsCommand command = new(
			Id: ValidId,
			CamCoordinates: camCoords,
			PanCoordinates: panCoords
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
