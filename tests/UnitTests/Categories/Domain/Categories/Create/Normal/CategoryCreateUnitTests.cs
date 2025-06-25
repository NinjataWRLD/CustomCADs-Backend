using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal;

using Data;

public class CategoryCreateUnitTests : CategoriesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CategoryCreatedValidData))]
	public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
	{
		CreateCategory(name, description);
	}

	[Theory]
	[ClassData(typeof(CategoryCreatedValidData))]
	public void Create_ShouldPopulatePropertiesProperly_WhenCategoryIsValid(string name, string description)
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
