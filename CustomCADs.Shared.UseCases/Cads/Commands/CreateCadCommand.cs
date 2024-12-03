namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record CreateCadCommand(
    string Key,
    string ContentType
) : ICommand<CadId>;
