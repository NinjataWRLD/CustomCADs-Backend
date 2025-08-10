using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;

using static CadsData;

public class CadCreateUnitTests : CadsBaseUnitTests
{
	private static readonly Coordinates coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		Cad.Create(ValidKey, ValidContentType, ValidVolume, coords, coords);
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = Cad.Create(ValidKey, ValidContentType, ValidVolume, coords, coords);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, cad.Key),
			() => Assert.Equal(ValidContentType, cad.ContentType),
			() => Assert.Equal(ValidVolume, cad.Volume),
			() => Assert.Equal(coords, cad.CamCoordinates),
			() => Assert.Equal(coords, cad.PanCoordinates)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CadCreateInvalidData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, decimal volume, Coordinates coords)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => Cad.Create(key, contentType, volume, coords, coords)
		);
	}
}
