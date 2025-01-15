namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadExistsByIdQuery(CadId Id) : IQuery<bool>;
