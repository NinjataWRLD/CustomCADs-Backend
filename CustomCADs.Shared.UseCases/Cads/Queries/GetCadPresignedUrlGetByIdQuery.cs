namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlGetByIdQuery(
    CadId Id
) : IQuery<string>;
