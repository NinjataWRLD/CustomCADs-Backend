namespace CustomCADs.Catalog.Application.Tags.Queries.GetById;

public record GetTagByIdQuery(
    TagId Id
) : IQuery<GetTagByIdDto>;
