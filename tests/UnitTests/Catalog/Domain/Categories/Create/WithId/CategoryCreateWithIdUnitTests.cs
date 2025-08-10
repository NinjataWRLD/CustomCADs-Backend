namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.WithId;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

public class CategoryCreateWithIdUnitTests : CategoriesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CategoryCreateValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
	{
		CreateCategory(name, description);
	}

	[Theory]
	[ClassData(typeof(CategoryCreateValidData))]
	public void CreateWithId_ShouldPopulateProperties_WhenCategoryIsValid(string name, string description)
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
	public void CreateWithId_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Category>>(
			() => CreateCategory(name, description)
		);
	}
}
