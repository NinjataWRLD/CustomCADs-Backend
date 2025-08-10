namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record CreateCadCommand(
	string Key,
	string ContentType,
	decimal Volume
) : ICommand<CadId>;
