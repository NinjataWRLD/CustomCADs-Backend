using CustomCADs.Catalog.Application.Tags.Queries.GetAll;
using CustomCADs.Catalog.Application.Tags.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Tags.Get.All;
using CustomCADs.Catalog.Endpoints.Tags.Get.Single;
using CustomCADs.Catalog.Endpoints.Tags.Post;

namespace CustomCADs.Catalog.Endpoints.Tags;

internal static class Mapper
{
    internal static CreateTagResponse ToCreateTagResponse(this GetTagByIdDto tag)
        => new(
            tag.Id.Value,
            tag.Name
        );

    internal static GetTagByIdResponse ToGetTagByIdResponse(this GetTagByIdDto tag)
        => new(
            tag.Id.Value,
            tag.Name
        );

    internal static GetAllTagsResponse ToGetAllTagsResponse(this GetAllTagsDto tag)
        => new(
            tag.Id.Value,
            tag.Name
        );
}
