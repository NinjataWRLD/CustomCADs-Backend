﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.ValueObjects;
using CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.CamCoordinates.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates;

public class CadPanCoordinatesUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadCamCoordinatesValidData))]
    public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetPanCoordinates(coordinates);
    }

    [Theory]
    [ClassData(typeof(CadCamCoordinatesValidData))]
    public void SetPanCoordinates_ShouldPopulateProperly_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetPanCoordinates(coordinates);

        Assert.Equal(coordinates, cad.PanCoordinates);
    }

    [Theory]
    [ClassData(typeof(CadCamCoordinatesInvalidData))]
    public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid(Coordinates coordinates)
    {
        var cad = CreateCad();

        Assert.Throws<CustomValidationException<Cad>>(() =>
        {
            cad.SetPanCoordinates(coordinates);
        });
    }
}
