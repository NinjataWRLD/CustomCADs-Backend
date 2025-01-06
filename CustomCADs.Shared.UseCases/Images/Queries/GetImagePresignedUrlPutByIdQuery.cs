namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlPutByIdQuery(
    ImageId Id,
    string NewContentType,
    string NewFileName
) : IQuery<string>;
