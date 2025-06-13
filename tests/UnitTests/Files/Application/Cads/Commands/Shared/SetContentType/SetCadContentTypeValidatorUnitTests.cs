using CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;

using static CadsData;

public class SetCadContentTypeValidatorUnitTests : CadsBaseUnitTests
{
	private readonly SetCadContentTypeValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenContentTypeIsValid()
	{
		// Arrange
		SetCadContentTypeCommand command = new(id, ValidContentType);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Fact]
	public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid()
	{
		// Arrange
		SetCadContentTypeCommand command = new(id, InvalidContentType);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ContentType);
	}
}
