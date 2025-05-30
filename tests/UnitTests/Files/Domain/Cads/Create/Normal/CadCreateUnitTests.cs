using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;

using Data;

public class CadCreateUnitTests : CadsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CadCreateValidData))]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid(string key, string contentType, decimal volume, int x, int y, int z)
	{
		Cad.Create(key, contentType, volume, new(x, y, z), new(x, y, z));
	}

	[Theory]
	[ClassData(typeof(CadCreateValidData))]
	public void Create_ShouldPopulatePropertiesProperly_WhenCadIsValid(string key, string contentType, decimal volume, int x, int y, int z)
	{
		var cad = Cad.Create(key, contentType, volume, new(x, y, z), new(x, y, z));

		Assert.Multiple(
			() => Assert.Equal(key, cad.Key),
			() => Assert.Equal(contentType, cad.ContentType),
			() => Assert.Equal(new(x, y, z), cad.CamCoordinates),
			() => Assert.Equal(new(x, y, z), cad.PanCoordinates)
		);
	}

	[Theory]
	[ClassData(typeof(CadCreateInvalidKeyData))]
	[ClassData(typeof(CadCreateInvalidContentTypeData))]
	[ClassData(typeof(CadCreateInvalidCoordsData))]
	[ClassData(typeof(CadCreateInvalidVolumeData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, decimal volume, int x, int y, int z)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => Cad.Create(key, contentType, volume, new(x, y, z), new(x, y, z))
		);
	}
}
