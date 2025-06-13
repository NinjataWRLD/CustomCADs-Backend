using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;

using static CategoriesData;

public class CreateCategoryValidatorUnitTests : CategoriesBaseUnitTests
{
	private readonly CreateCategoryValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenCategoryIsValid()
	{
		// Arrange
		CategoryWriteDto category = new(ValidName, ValidDescription);
		CreateCategoryCommand command = new(category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateCategoryInvalidData))]
	public void Validate_ShouldBeInvalid_WhenCategoryIsNotValid(string name, string description)
	{
		// Arrange
		CategoryWriteDto category = new(name, description);
		CreateCategoryCommand command = new(category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.False(result.IsValid);
	}
}
