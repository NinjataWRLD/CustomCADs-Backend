namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadVolumeByIdQuery(
	CadId Id
) : IQuery<decimal>;
