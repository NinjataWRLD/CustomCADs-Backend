using CustomCADs.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;

public record GetAllTagsQuery
 : IQuery<TagReadDto[]>;
