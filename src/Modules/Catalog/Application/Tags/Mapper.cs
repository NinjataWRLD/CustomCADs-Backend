using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags;

internal static class Mapper
{
	internal static TagReadDto ToDto(this Tag tag)
		=> new(
			Id: tag.Id,
			Name: tag.Name
		);
}
