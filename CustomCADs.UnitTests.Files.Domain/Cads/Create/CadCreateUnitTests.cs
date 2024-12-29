using CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create;

public class CadCreateData : TheoryData<string, string, int, int, int>;

public class CadCreateUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadCreateValidData))]
    public void Create_ShouldNotThrowExcepion_WhenCadIsValid(string key, string contentType, int x, int y, int z)
    {
        Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
    }

    [Theory]
    [ClassData(typeof(CadCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenCadIsValid(string key, string contentType, int x, int y, int z)
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
    [ClassData(typeof(CadCreateInvalidKeyData))]
    public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }

    [Theory]
    [ClassData(typeof(CadCreateInvalidContentTypeData))]
    public void Create_ShouldThrowException_WhenContentTypeIsInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }

    [Theory]
    [ClassData(typeof(CadCreateInvalidCoordsData))]
    public void Create_ShouldThrowException_WhenCoordsAreInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }
}
