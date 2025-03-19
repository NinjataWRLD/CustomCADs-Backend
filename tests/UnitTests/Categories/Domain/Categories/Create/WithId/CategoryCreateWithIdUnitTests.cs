using CustomCADs.Categories.Domain.Categories.Exceptions;
using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId;

public class CategoryCreateWithIdUnitTests : CategoriesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CategoryCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
    {
        CreateCategory(name, description);
    }

    [Theory]
    [ClassData(typeof(CategoryCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenCategoryIsValid(string name, string description)
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
    public void CreateWithId_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
    {
        Assert.Throws<CategoryValidationException>(() =>
        {
            CreateCategory(name, description);
        });
    }
}
