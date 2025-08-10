namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record SetCadVolumeCommand(
	CadId Id,
	decimal Volume
) : ICommand;
