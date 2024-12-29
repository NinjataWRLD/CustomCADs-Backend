using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties;

public class CadPanCoordinatesUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(CadValidCoord1, CadValidCoord1, CadValidCoord1)]
    public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid(int x, int y, int z)
    {
        var cad = CreateCad();

        cad.SetPanCoordinates(new(x, y, z));
    }

    [Theory]
    [InlineData(CadValidCoord1, CadValidCoord1, CadValidCoord1)]
    public void SetPanCoordinates_ShouldPopulateProperly_WhenCoordinatesAreValid(int x, int y, int z)
    {
        var cad = CreateCad();
        Coordinates coordinates = new(x, y, z);

        cad.SetPanCoordinates(coordinates);

        Assert.Equal(coordinates, cad.PanCoordinates);
    }

    [Theory]
    [InlineData(CadInvalidCoord1, CadInvalidCoord1, CadInvalidCoord1)]
    public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid(int x, int y, int z)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetPanCoordinates(new(x, y, z));
        });
    }
}
