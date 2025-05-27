namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record SetCadContentTypeCommand(
	CadId Id,
	string ContentType
) : ICommand;
