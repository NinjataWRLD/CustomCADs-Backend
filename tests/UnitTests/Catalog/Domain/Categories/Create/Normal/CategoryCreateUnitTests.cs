namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.Normal;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

public class CategoryCreateUnitTests : CategoriesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CategoryCreateValidData))]
	public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
	{
		CreateCategory(name, description);
	}

	[Theory]
	[ClassData(typeof(CategoryCreateValidData))]
	public void Create_ShouldPopulateProperties_WhenCategoryIsValid(string name, string description)
	{
		var category = CreateCategory(name, description);

		Assert.Multiple(
			() => Assert.Equal(category.Name, name),
			() => Assert.Equal(category.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(CategoryCreateInvalidNameData))]
	[ClassData(typeof(CategoryCreateInvalidDescriptionData))]
	public void Create_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Category>>(
			() => CreateCategory(name, description)
		);
	}
}
