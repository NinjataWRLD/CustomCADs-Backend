namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

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

		Assert.Throws<CustomValidationException<Category>>(
			() => category.SetName(name)
		);
	}
}
