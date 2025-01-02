using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Name.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Name;

public class CategoryNameUnitTests : CategoriesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CategoryNameValidData))]
    public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
    {
        var role = CreateCategory();

        role.SetName(name);
    }

    [Theory]
    [ClassData(typeof(CategoryNameValidData))]
    public void SetName_SetsName_WhenNameIsValid(string name)
    {
        var category = CreateCategory();

        category.SetName(name);

        Assert.Equal(category.Name, name);
    }

    [Theory]
    [ClassData(typeof(CategogryNameInvalidData))]
    public void SetName_ThrowsException_WhenNameIsInvalid(string name)
    {
        var category = CreateCategory();

        Assert.Throws<CategoryValidationException>(() =>
        {
            category.SetName(name);
        });
    }
}
