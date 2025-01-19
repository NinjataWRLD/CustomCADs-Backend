namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record SetCadKeyCommand(
    CadId Id,
    string Key
) : ICommand;
