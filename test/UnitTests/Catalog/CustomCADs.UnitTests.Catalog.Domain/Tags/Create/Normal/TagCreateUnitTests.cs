﻿using CustomCADs.Catalog.Domain.Common.Exceptions.Tags;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal.Data;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal;

public class TagCreateUnitTests : TagsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(TagCreateValidData))]
    public void Create_ShouldNotThrowException_WhenProductIsValid(string name)
    {
        CreateTag(name);
    }

    [Theory]
    [ClassData(typeof(TagCreateValidData))]
    public void Create_ShouldPopulateProperly_WhenProductIsValid(string name)
    {
        Tag tag = CreateTag(name);

        Assert.Equal(name, tag.Name);
    }

    [Theory]
    [ClassData(typeof(TagCreateInvalidNameData))]
    public void Create_ShouldThrowException_WhenProductIsNotValid(string name)
    {
        Assert.Throws<TagValidationException>(() =>
        {
            CreateTag(name);
        });
    }
}
