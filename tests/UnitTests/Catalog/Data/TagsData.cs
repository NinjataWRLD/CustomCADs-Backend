using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Data;

using static TagConstants;

public static class TagsData
{
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public const string InvalidName = "";
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);

	public static readonly TagId ValidId = TagId.New();
}
