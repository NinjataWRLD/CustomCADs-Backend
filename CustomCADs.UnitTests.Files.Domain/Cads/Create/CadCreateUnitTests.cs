namespace CustomCADs.UnitTests.Files.Domain.Cads.Create;

public class CadCreateUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(ValidKey1, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1)]
    [InlineData(ValidKey2, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2)]
    public void Create_ShouldNotThrowExcepion_WhenCadIsValid(string key, string contentType, int x, int y, int z)
    {
        Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
    }

    [Theory]
    [InlineData(ValidKey1, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1)]
    [InlineData(ValidKey2, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2)]
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
    [InlineData(InvalidKey, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1)]
    [InlineData(InvalidKey, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2)]
    public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }

    [Theory]
    [InlineData(ValidKey1, InvalidContentType, ValidCoord1, ValidCoord1, ValidCoord1)]
    [InlineData(ValidKey2, InvalidContentType, ValidCoord2, ValidCoord2, ValidCoord2)]
    public void Create_ShouldThrowException_WhenContentTypeIsInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }

    [Theory]
    [InlineData(ValidKey1, InvalidContentType, InvalidCoord1, InvalidCoord1, InvalidCoord1)]
    [InlineData(ValidKey2, InvalidContentType, InvalidCoord2, InvalidCoord2, InvalidCoord2)]
    public void Create_ShouldThrowException_WhenCoordsAreInvalid(string key, string contentType, int x, int y, int z)
    {
        Assert.Throws<CadValidationException>(() =>
        {
            Cad.Create(key, contentType, new(x, y, z), new(x, y, z));
        });
    }
}
