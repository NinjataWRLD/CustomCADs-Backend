using CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;

using static CategoriesData;

public class EditCategoryValidatorUnitTests : CategoriesBaseUnitTests
{
	private readonly EditCategoryValidator validator = new();
	private readonly CategoryId id = CategoryId.New();

	[Fact]
	public void Validator_ShouldBeValid_WhenCategoryIsValid()
	{
		// Arrange
		CategoryWriteDto category = new(ValidName, ValidDescription);
		EditCategoryCommand command = new(id, category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.EditCategoryInvalidData))]
	public void Validator_ShouldBeInvalid_WhenCategoryIsNotValid(string name, string description)
	{
		// Arrange
		CategoryWriteDto category = new(name, description);
		EditCategoryCommand command = new(id, category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.False(result.IsValid);
	}
}
