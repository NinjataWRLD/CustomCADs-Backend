using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create;

public class RoleCreateData : TheoryData<string, string>;

public class RoleCreateUnitTests : CategoriesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CategoryCreateValidData))]
    public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
    {
        CreateCategory(name, description);
    }

    [Theory]
    [ClassData(typeof(CategoryCreateValidData))]
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
    [ClassData(typeof(CategoryCreateInvalidNameData))]
    public void Create_ShouldThrowException_WhenNameIsInvalid(string name, string description)
    {
        Assert.Throws<CategoryValidationException>(() =>
        {
            CreateCategory(name, description);
        });
    }

    [Theory]
    [ClassData(typeof(CategoryCreateInvalidDescriptionData))]
    public void Create_ShouldThrowException_WhenDescriptionIsInvalid(string name, string description)
    {
        Assert.Throws<CategoryValidationException>(() =>
        {
            CreateCategory(name, description);
        });
    }
}
