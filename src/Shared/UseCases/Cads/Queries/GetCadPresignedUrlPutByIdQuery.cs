namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlPutByIdQuery(
    CadId Id,
    string NewContentType,
    string NewFileName
) : IQuery<string>;
