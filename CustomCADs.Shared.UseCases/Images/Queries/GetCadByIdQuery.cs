namespace CustomCADs.Shared.UseCases.Images.Queries;

public sealed record GetImageByIdQuery(
    ImageId Id
) : IQuery<(ImageId Id, string Key, string ContentType)>;
