using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Description.Data;

namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Description;

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

		Assert.Throws<CustomValidationException<Category>>(() =>
		{
			category.SetDescription(description);
		});
	}
}
