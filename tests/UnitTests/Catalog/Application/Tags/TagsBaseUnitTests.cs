using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Tags;

using static TagsData;

public class TagsBaseUnitTests
{
    public static readonly CancellationToken ct = CancellationToken.None;

    protected static Tag CreateTag(TagId? id = null, string? name = null)
        => Tag.CreateWithId(id ?? ValidId, name ?? ValidName1);
}
