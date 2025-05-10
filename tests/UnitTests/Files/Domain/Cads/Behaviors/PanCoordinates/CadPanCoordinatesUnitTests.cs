using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates;

using Data;

public class CadPanCoordinatesUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadPanCoordinatesValidData))]
    public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetPanCoordinates(coordinates);
    }

    [Theory]
    [ClassData(typeof(CadPanCoordinatesValidData))]
    public void SetPanCoordinates_ShouldPopulateProperly_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetPanCoordinates(coordinates);

        Assert.Equal(coordinates, cad.PanCoordinates);
    }

    [Theory]
    [ClassData(typeof(CadPanCoordinatesInvalidData))]
    public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid(Coordinates coordinates)
    {
        var cad = CreateCad();

        Assert.Throws<CustomValidationException<Cad>>(() =>
        {
            cad.SetPanCoordinates(coordinates);
        });
    }
}
