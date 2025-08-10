namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadExistsByIdQuery(CadId Id) : IQuery<bool>;
