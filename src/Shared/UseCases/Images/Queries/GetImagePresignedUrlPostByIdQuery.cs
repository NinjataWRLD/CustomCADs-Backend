namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlPostByIdQuery(
    string Name,
    string ContentType,
    string FileName
) : IQuery<(string, string)>;
