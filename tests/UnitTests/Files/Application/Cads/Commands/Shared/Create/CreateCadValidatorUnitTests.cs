using CustomCADs.Files.Application.Cads.Commands.Shared.Create;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create;

using static CadsData;

public class CreateCadValidatorUnitTests : CadsBaseUnitTests
{
	private readonly CreateCadValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenCadIsValid()
	{
		// Arrange
		CreateCadCommand command = new(ValidKey, ValidContentType, ValidVolume);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateCadInvalidData))]
	public void Validate_ShouldBeInvalid_WhenCadIsNotValid(string key, string contentType, decimal volume)
	{
		// Arrange
		CreateCadCommand command = new(key, contentType, volume);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.False(result.IsValid);
	}
}
