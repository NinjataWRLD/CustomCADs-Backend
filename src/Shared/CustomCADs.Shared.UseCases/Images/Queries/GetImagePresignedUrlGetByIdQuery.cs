namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlGetByIdQuery(
    ImageId Id
) : IQuery<string>;
