namespace CustomCADs.Shared.UseCases.Images.Queries;

public sealed record GetImagesByIdsQuery(
    ImageId[] Ids
) : IQuery<Dictionary<ImageId, (string Key, string ContentType)>>;
