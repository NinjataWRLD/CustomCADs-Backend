using CustomCADs.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

public record GetTagByIdQuery(
	TagId Id
) : IQuery<TagReadDto>;
