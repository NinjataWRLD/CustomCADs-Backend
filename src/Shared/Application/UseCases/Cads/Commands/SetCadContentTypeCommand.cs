namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record SetCadContentTypeCommand(
	CadId Id,
	string ContentType
) : ICommand;
