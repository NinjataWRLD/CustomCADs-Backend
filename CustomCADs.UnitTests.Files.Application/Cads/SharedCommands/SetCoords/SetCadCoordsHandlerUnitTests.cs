using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetCoords;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetCoords.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetCoords;

public class SetCadCoordsHandlerData : TheoryData<int, int, int, int, int, int>;

public class SetCadCoordsHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly Cad cad = CreateCad();

    [Theory]
    [ClassData(typeof(SetCadCoordsHandlerValidData))]
    public async Task Handle_ShouldQueryDatabase(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadCoordsCommand command = new(
            Id: id,
            CamCoordinates: new(x1, y1, z1),
            PanCoordinates: new(x2, y2, z2)
        );
        SetCadCoordsHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
    }
    
    [Theory]
    [ClassData(typeof(SetCadCoordsHandlerValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);

        SetCadCoordsCommand command = new(
            Id: id,
            CamCoordinates: new(x1, y1, z1),
            PanCoordinates: new(x2, y2, z2)
        );
        SetCadCoordsHandler handler = new(reads, uow);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(SetCadCoordsHandlerValidData))]
    public async Task Handle_ShouldModifyCad_WhenCadFound(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(cad);
        CoordinatesDto camCoords = new(x1, y1, z1);
        CoordinatesDto panCoords = new(x2, y2, z2);

        SetCadCoordsCommand command = new(
            Id: id,
            CamCoordinates: camCoords,
            PanCoordinates: panCoords
        );
        SetCadCoordsHandler handler = new(reads, uow);

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
    [ClassData(typeof(SetCadCoordsHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenCadNotFound(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(null as Cad);
        CoordinatesDto camCoords = new(x1, y1, z1);
        CoordinatesDto panCoords = new(x2, y2, z2);

        SetCadCoordsCommand command = new(
            Id: id,
            CamCoordinates: camCoords,
            PanCoordinates: panCoords
        );
        SetCadCoordsHandler handler = new(reads, uow);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
