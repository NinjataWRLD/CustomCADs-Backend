namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record SetCadVolumeCommand(
	CadId Id,
	decimal Volume
) : ICommand;
