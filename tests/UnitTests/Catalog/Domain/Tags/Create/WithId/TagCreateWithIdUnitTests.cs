using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId;

using Data;
using static TagsData;

public class TagCreateWithIdUnitTests : TagsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(TagCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenProductIsValid(string name)
    {
        CreateTagWithId(ValidId, name);
    }

    [Theory]
    [ClassData(typeof(TagCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateProperly_WhenProductIsValid(string name)
    {
        Tag tag = CreateTagWithId(ValidId, name);

        Assert.Equal(name, tag.Name);
    }

    [Theory]
    [ClassData(typeof(TagCreateWithIdInvalidNameData))]
    public void CreateWithId_ShouldThrowException_WhenProductIsNotValid(string name)
    {
        Assert.Throws<CustomValidationException<Tag>>(() =>
        {
            CreateTagWithId(ValidId, name);
        });
    }
}
