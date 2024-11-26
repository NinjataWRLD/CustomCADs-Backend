using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Commands;

public record SetCadCoordsCommand(
    CadId Id,
    CoordinatesDto? CamCoordinates,
    CoordinatesDto? PanCoordinates
) : ICommand;
