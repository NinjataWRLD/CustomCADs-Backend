using CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords;

public class SetCadCoordsValidatorUnitTests : CadsBaseUnitTests
{
	private readonly SetCadCoordsValidator validator = new();

	[Theory]
	[ClassData(typeof(SetCadCoordsValidData))]
	public void Validate_ShouldBeValid_WhenCoordsIsValid(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		SetCadCoordsCommand command = new(id1, new(x1, y1, z1), new(x2, y2, z2));

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(SetCadCoordsInvalidCamData))]
	public void Validate_ShouldReturnProperErrors_WhenCamCoordsIsNotValid(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		SetCadCoordsCommand command = new(id1, new(x1, y1, z1), new(x2, y2, z2));

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.X);
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.Y);
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.Z);
	}

	[Theory]
	[ClassData(typeof(SetCadCoordsInvalidPanData))]
	public void Validate_ShouldReturnProperErrors_WhenPanCoordsIsNotValid(int x1, int y1, int z1, int x2, int y2, int z2)
	{
		// Arrange
		SetCadCoordsCommand command = new(id1, new(x1, y1, z1), new(x2, y2, z2));

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.X);
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.Y);
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.Z);
	}
}
