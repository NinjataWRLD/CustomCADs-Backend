using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties;

public class CadCamCoordinatesUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(ValidCoord1, ValidCoord1, ValidCoord1)]
    public void SetCamCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid(int x, int y, int z)
    {
        var cad = CreateCad();

        cad.SetCamCoordinates(new(x, y, z));
    }

    [Theory]
    [InlineData(ValidCoord1, ValidCoord1, ValidCoord1)]
    public void SetCamCoordinates_ShouldPopulateProperly_WhenCoordinatesAreValid(int x, int y, int z)
    {
        var cad = CreateCad();
        Coordinates coordinates = new(x, y, z);

        cad.SetCamCoordinates(coordinates);

        Assert.Equal(coordinates, cad.CamCoordinates);
    }

    [Theory]
    [InlineData(InvalidCoord1, InvalidCoord1, InvalidCoord1)]
    public void SetCamCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid(int x, int y, int z)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetCamCoordinates(new(x, y, z));
        });
    }
}
