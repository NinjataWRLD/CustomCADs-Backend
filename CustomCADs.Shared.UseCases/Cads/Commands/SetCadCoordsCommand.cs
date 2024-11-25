using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;

namespace CustomCADs.Shared.UseCases.Cads.Commands;

public record SetCadCoordsCommand(
    CadId Id,
    CoordinatesDto? CamCoordinates,
    CoordinatesDto? PanCoordinates
) : ICommand;
