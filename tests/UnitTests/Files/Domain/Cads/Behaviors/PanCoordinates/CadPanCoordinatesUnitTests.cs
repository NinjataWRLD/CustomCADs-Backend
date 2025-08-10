using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates;

using static CadsData;

public class CadPanCoordinatesUnitTests : CadsBaseUnitTests
{
	private static readonly Coordinates coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(coords);
	}

	[Fact]
	public void SetPanCoordinates_ShouldPopulateProperties_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(coords);

		Assert.Equal(coords, cad.PanCoordinates);
	}

	[Fact]
	public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetPanCoordinates(coords with { X = MaxInvalidCoord })
		);
	}
}
