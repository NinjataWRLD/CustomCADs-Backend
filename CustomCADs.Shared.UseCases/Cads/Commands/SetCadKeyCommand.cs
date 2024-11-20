namespace CustomCADs.Shared.UseCases.Cads.Commands;

public record SetCadKeyCommand(
    CadId Id,
    string Key
) : ICommand;
