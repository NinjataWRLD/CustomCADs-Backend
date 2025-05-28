using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

using Data;

public class CadCreateWithIdUnitTests : CadsBaseUnitTests
{
    private static readonly CadId id = CadId.New();

    [Theory]
    [ClassData(typeof(CadCreateValidData))]
    public void CreateWithId_ShouldNotThrowExcepion_WhenCadIsValid(string key, string contentType, decimal volume, int x, int y, int z)
    {
        Cad.CreateWithId(id, key, contentType, volume, new(x, y, z), new(x, y, z));
    }

    [Theory]
    [ClassData(typeof(CadCreateValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenCadIsValid(string key, string contentType, decimal volume, int x, int y, int z)
    {
        var cad = Cad.CreateWithId(id, key, contentType, volume, new(x, y, z), new(x, y, z));

        Assert.Multiple(
            () => Assert.Equal(id, cad.Id),
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
    public void CreateWithId_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, decimal volume, int x, int y, int z)
    {
        Assert.Throws<CustomValidationException<Cad>>(
            () => Cad.CreateWithId(id, key, contentType, volume, new(x, y, z), new(x, y, z))
        );
    }
}
