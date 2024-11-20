using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Shared.Commands.Cads;

public record SetCadCoordsCommand(
    CadId Id,
    CoordinatesDto? CamCoordinates,
    CoordinatesDto? PanCoordinates
) : ICommand;
