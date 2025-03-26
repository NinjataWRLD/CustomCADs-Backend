using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;
using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags;

internal static class Mapper
{
    internal static GetAllTagsDto ToGetAllDto(this Tag tag)
        => new(
            Id: tag.Id,
            Name: tag.Name
        );

    internal static GetTagByIdDto ToGetByIdDto(this Tag tag)
        => new(
            Id: tag.Id,
            Name: tag.Name
        );
}
