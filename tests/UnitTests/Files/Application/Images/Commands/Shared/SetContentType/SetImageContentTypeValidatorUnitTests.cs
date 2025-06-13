using CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetContentType;

using static ImagesData;

public class SetImageContentTypeValidatorUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageContentTypeValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenContentTypeIsValid()
	{
		// Arrange
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Fact]
	public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid()
	{
		// Arrange
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ContentType);
	}
}
