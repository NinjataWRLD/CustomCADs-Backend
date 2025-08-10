using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags;

using static TagsData;

public class TagsBaseUnitTests
{
	protected static Tag CreateTag(
		string? name = null
	) => Tag.Create(
			name: name ?? MinValidName
		);

	protected static Tag CreateTagWithId(
		TagId? id = null,
		string? name = null
	) => Tag.CreateWithId(
			id: id ?? ValidId,
			name: name ?? MinValidName
		);
}
