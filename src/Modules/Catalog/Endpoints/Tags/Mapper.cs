using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.All;
using CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.Single;
using CustomCADs.Catalog.Endpoints.Tags.Endpoints.Post;

namespace CustomCADs.Catalog.Endpoints.Tags;

internal static class Mapper
{
	internal static CreateTagResponse ToCreateTagResponse(this TagReadDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);

	internal static GetTagByIdResponse ToGetTagByIdResponse(this TagReadDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);

	internal static GetAllTagsResponse ToGetAllTagsResponse(this TagReadDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);
}
