using CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords;

using static CadsData;

public class SetCadCoordsValidatorUnitTests : CadsBaseUnitTests
{
	private readonly SetCadCoordsValidator validator = new();

	private static readonly CoordinatesDto camCoords = new(MinValidCoord, MinValidCoord, MinValidCoord);
	private static readonly CoordinatesDto panCoords = new(MaxValidCoord, MaxValidCoord, MaxValidCoord);

	[Fact]
	public void Validate_ShouldBeValid_WhenCoordsIsValid()
	{
		// Arrange
		SetCadCoordsCommand command = new(id, camCoords, panCoords);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Fact]
	public void Validate_ShouldReturnProperErrors_WhenCamCoordsIsNotValid()
	{
		// Arrange
		SetCadCoordsCommand command = new(id, camCoords with { X = MinInvalidCoord }, panCoords);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.X);
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.Y);
		result.ShouldHaveValidationErrorFor(x => x.CamCoordinates!.Z);
	}

	[Fact]
	public void Validate_ShouldReturnProperErrors_WhenPanCoordsIsNotValid()
	{
		// Arrange
		SetCadCoordsCommand command = new(id, camCoords, panCoords with { X = MaxInvalidCoord });

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.X);
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.Y);
		result.ShouldHaveValidationErrorFor(x => x.PanCoordinates!.Z);
	}
}
