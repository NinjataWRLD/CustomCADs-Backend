using CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

public class CadCreateWithIdUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowExcepion_WhenCadIsValid(string key, string contentType, int x, int y, int z)
    {
        Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
    }

    [Theory]
    [ClassData(typeof(CadCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenCadIsValid(string key, string contentType, int x, int y, int z)
    {
        var cad = Cad.Create(key, contentType, new(x, y, z), new(x, y, z));

        Assert.Multiple(
            () => Assert.Equal(key, cad.Key),
            () => Assert.Equal(contentType, cad.ContentType),
            () => Assert.Equal(new(x, y, z), cad.CamCoordinates),
            () => Assert.Equal(new(x, y, z), cad.PanCoordinates)
        );
    }

    [Theory]
    [ClassData(typeof(CadCreateWithIdInvalidKeyData))]
    [ClassData(typeof(CadCreateWithIdInvalidContentTypeData))]
    [ClassData(typeof(CadCreateWithIdInvalidCoordsData))]
    public void CreateWithId_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }
}
