using CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;

using Data;

public class SetCadContentTypeValidatorUnitTests : CadsBaseUnitTests
{
	private readonly SetCadContentTypeValidator validator = new();

	[Theory]
	[ClassData(typeof(SetCadContentTypeValidData))]
	public void Validate_ShouldBeValid_WhenContentTypeIsValid(string contentType)
	{
		// Arrange
		SetCadContentTypeCommand command = new(id1, contentType);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(SetCadContentTypeInvalidData))]
	public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string contentType)
	{
		// Arrange
		SetCadContentTypeCommand command = new(id1, contentType);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ContentType);
	}
}
