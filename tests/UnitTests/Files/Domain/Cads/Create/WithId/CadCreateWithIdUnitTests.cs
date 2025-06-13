using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

using static CadsData;

public class CadCreateWithIdUnitTests : CadsBaseUnitTests
{
	private static readonly CadId id = CadId.New();
	private static readonly Coordinates coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		Cad.CreateWithId(id, ValidKey, ValidContentType, ValidVolume, coords, coords);
	}

	[Fact]
	public void CreateWithId_ShouldPopulatePropertiesProperly_WhenCadIsValid()
	{
		var cad = Cad.CreateWithId(id, ValidKey, ValidContentType, ValidVolume, coords, coords);

		Assert.Multiple(
			() => Assert.Equal(id, cad.Id),
			() => Assert.Equal(ValidKey, cad.Key),
			() => Assert.Equal(ValidContentType, cad.ContentType),
			() => Assert.Equal(ValidVolume, cad.Volume),
			() => Assert.Equal(coords, cad.CamCoordinates),
			() => Assert.Equal(coords, cad.PanCoordinates)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CadCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, decimal volume, Coordinates coords)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => Cad.CreateWithId(id, key, contentType, volume, coords, coords)
		);
	}
}
