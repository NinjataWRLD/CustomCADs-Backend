using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.CamCoordinates;

using static CadsData;

public class CadCamCoordinatesUnitTests : CadsBaseUnitTests
{
	private static readonly Coordinates coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void SetCamCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetCamCoordinates(coords);
	}

	[Fact]
	public void SetCamCoordinates_ShouldPopulateProperties_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetCamCoordinates(coords);

		Assert.Equal(coords, cad.CamCoordinates);
	}

	[Fact]
	public void SetCamCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetCamCoordinates(coords with { X = MinInvalidCoord })
		);
	}
}
