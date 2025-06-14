using CustomCADs.Files.Application.Images.Commands.Shared.Create;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create;

using static ImagesData;

public class CreateImageValidatorUnitTests : ImagesBaseUnitTests
{
	private readonly CreateImageValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenImageIsValid()
	{
		// Arrange
		CreateImageCommand command = new(ValidKey, ValidContentType);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateImageInvalidData))]
	public void Validate_ShouldBeInvalid_WhenImageIsNotValid(string key, string contentType)
	{
		// Arrange
		CreateImageCommand command = new(key, contentType);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.False(result.IsValid);
	}
}
