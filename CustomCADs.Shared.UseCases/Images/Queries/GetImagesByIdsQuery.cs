namespace CustomCADs.Shared.UseCases.Images.Queries;

public sealed record GetImagesByIdsQuery(
    ImageId[] Ids
) : IQuery<(ImageId Id, string Key, string ContentType)[]>;
