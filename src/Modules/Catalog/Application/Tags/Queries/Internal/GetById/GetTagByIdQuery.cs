namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

public record GetTagByIdQuery(
    TagId Id
) : IQuery<GetTagByIdDto>;
