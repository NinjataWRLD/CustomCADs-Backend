using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record SetCadCoordsCommand(
	CadId Id,
	CoordinatesDto? CamCoordinates,
	CoordinatesDto? PanCoordinates
) : ICommand;
