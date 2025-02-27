namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlPostByIdQuery(
    string Name,
    string ContentType,
    string FileName
) : IQuery<(string, string)>;
