using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;

public class CreateCategoryValidatorUnitTests : CategoriesBaseUnitTests
{
	private readonly CreateCategoryValidator validator = new();

	[Theory]
	[ClassData(typeof(CreateCategoryValidData))]
	public void Validate_ShouldBeValid_WhenCategoryIsValid(string name, string description)
	{
		// Arrange
		CategoryWriteDto category = new(name, description);
		CreateCategoryCommand command = new(category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(CreateCategoryInvalidNameData))]
	[ClassData(typeof(CreateCategoryInvalidDescriptionData))]
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

	[Theory]
	[ClassData(typeof(CreateCategoryInvalidNameData))]
	public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description)
	{
		// Arrange
		CategoryWriteDto category = new(name, description);
		CreateCategoryCommand command = new(category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
	}

	[Theory]
	[ClassData(typeof(CreateCategoryInvalidDescriptionData))]
	public void Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description)
	{
		// Arrange
		CategoryWriteDto category = new(name, description);
		CreateCategoryCommand command = new(category);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
	}
}
