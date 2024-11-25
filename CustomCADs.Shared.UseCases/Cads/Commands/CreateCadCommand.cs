using CustomCADs.Shared.Core.Common.TypedIds.Cads;

namespace CustomCADs.Shared.UseCases.Cads.Commands;

public record CreateCadCommand(
    string Key,
    string ContentType
) : ICommand<CadId>;
