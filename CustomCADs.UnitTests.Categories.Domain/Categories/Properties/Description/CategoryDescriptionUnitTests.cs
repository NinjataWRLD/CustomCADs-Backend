using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Description.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Description;

public class CategoryDescriptionUnitTests : CategoriesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CategoryCreateValidData))]
    public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
    {
        var category = CreateCategory();

        category.SetDescription(description);
    }

    [Theory]
    [ClassData(typeof(CategoryCreateValidData))]
    public void SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
    {
        var category = CreateCategory();

        category.SetDescription(description);

        Assert.Equal(category.Description, description);
    }

    [Theory]
    [ClassData(typeof(CategoryCreateInvalidData))]
    public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
    {
        var category = CreateCategory();

        Assert.Throws<CategoryValidationException>(() =>
        {
            category.SetDescription(description);
        });
    }
}
