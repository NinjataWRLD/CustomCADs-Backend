namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadVolumeByIdQuery(
    CadId Id
) : IQuery<decimal>;
