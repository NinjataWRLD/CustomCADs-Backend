namespace CustomCADs.Shared.UseCases.Cads.Commands;

public sealed record CreateCadCommand(
    string Key,
    string ContentType,
    decimal Volume
) : ICommand<CadId>;
