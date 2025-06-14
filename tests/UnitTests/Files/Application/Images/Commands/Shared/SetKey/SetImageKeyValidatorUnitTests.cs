using CustomCADs.Files.Application.Images.Commands.Shared.SetKey;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetKey;

using static ImagesData;

public class SetImageKeyValidatorUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageKeyValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenKeyIsValid()
	{
		// Arrange
		SetImageKeyCommand command = new(id, ValidKey);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Fact]
	public void Validate_ShouldReturnProperErrors_WhenKeyIsNotValid()
	{
		// Arrange
		SetImageKeyCommand command = new(id, InvalidKey);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Key);
	}
}
