using CustomCADs.Catalog.Application.Tags.Queries.GetAll;
using CustomCADs.Catalog.Application.Tags.Queries.GetById;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags;

internal static class Mapper
{
    internal static GetTagByIdDto ToGetTagByIdDto(this Tag tag)
        => new(
            Id: tag.Id,
            Name: tag.Name
        );

    internal static GetAllTagsDto ToGetAllTagsDto(this Tag tag)
        => new(
            Id: tag.Id,
            Name: tag.Name
        );
}
