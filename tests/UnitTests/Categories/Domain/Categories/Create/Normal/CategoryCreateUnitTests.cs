﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal;

public class CategoryCreateUnitTests : CategoriesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CategoryCreateWithIdValidData))]
    public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
    {
        CreateCategory(name, description);
    }

    [Theory]
    [ClassData(typeof(CategoryCreateWithIdValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenCategoryIsValid(string name, string description)
    {
        var category = CreateCategory(name, description);

        Assert.Multiple(() =>
        {
            Assert.Equal(category.Name, name);
            Assert.Equal(category.Description, description);
        });
    }

    [Theory]
    [ClassData(typeof(CategoryCreateWithIdInvalidNameData))]
    [ClassData(typeof(CategoryCreateWithIdInvalidDescriptionData))]
    public void Create_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
    {
        Assert.Throws<CustomValidationException<Category>>(() =>
        {
            CreateCategory(name, description);
        });
    }
}
