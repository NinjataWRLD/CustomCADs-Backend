using CustomCADs.Catalog.Domain.Common.Exceptions.Tags;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId;

public class TagCreateWithIdUnitTests : TagsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(TagCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenProductIsValid(TagId id, string name)
    {
        CreateTagWithId(id, name);
    }

    [Theory]
    [ClassData(typeof(TagCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateProperly_WhenProductIsValid(TagId id, string name)
    {
        Tag tag = CreateTagWithId(id, name);

        Assert.Equal(name, tag.Name);
    }

    [Theory]
    [ClassData(typeof(TagCreateWithIdInvalidNameData))]
    public void CreateWithId_ShouldThrowException_WhenProductIsNotValid(TagId id, string name)
    {
        Assert.Throws<TagValidationException>(() =>
        {
            CreateTagWithId(id, name);
        });
    }
}
