namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public record DuplicateCadsByIdsCommand(
	CadId[] Ids
) : ICommand<Dictionary<CadId, CadId>>;
