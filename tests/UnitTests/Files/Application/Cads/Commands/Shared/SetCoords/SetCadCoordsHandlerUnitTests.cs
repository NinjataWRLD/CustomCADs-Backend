using CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords;

using Data;

public class SetCadCoordsHandlerUnitTests : CadsBaseUnitTests
{
	private readonly SetCadCoordsHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Cad cad = CreateCad();

	public SetCadCoordsHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(cad);
	}

	[Theory]
	[ClassData(typeof(SetCadCoordsValidData))]
	public async Task Handle_ShouldQueryDatabase(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: id1,
			CamCoordinates: new(x1, y1, z1),
			PanCoordinates: new(x2, y2, z2)
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetCadCoordsValidData))]
	public async Task Handle_ShouldPersistToDatabase_WhenCadFound(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		SetCadCoordsCommand command = new(
			Id: id1,
			CamCoordinates: new(x1, y1, z1),
			PanCoordinates: new(x2, y2, z2)
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetCadCoordsValidData))]
	public async Task Handle_ShouldModifyCad_WhenCadFound(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		CoordinatesDto camCoords = new(x1, y1, z1);
		CoordinatesDto panCoords = new(x2, y2, z2);

		SetCadCoordsCommand command = new(
			Id: id1,
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

	[Theory]
	[ClassData(typeof(SetCadCoordsValidData))]
	public async Task Handle_ShouldThrowException_WhenCadNotFound(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(null as Cad);

		CoordinatesDto camCoords = new(x1, y1, z1);
		CoordinatesDto panCoords = new(x2, y2, z2);

		SetCadCoordsCommand command = new(
			Id: id1,
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
