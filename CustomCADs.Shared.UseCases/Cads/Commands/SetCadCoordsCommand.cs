using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record SetCadCoordsCommand(
    CadId Id,
    CoordinatesDto? CamCoordinates,
    CoordinatesDto? PanCoordinates
) : ICommand;
