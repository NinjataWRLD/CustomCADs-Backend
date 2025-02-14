using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Data;

using static TagConstants;

public static class TagsData
{
    public static readonly TagId ValidId = TagId.New();

    public static readonly string ValidName1 = new('a', NameMinLength + 1);
    public static readonly string ValidName2 = new('a', NameMaxLength - 1);
    public static readonly string InvalidName1 = new('a', NameMinLength - 1);
    public static readonly string InvalidName2 = new('a', NameMaxLength + 1);
    public const string InvalidName3 = "";
}
