﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.ValueObjects;
using CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.CamCoordinates.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.CamCoordinates;

public class CadCamCoordinatesUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadCamCoordinatesValidData))]
    public void SetCamCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetCamCoordinates(coordinates);
    }

    [Theory]
    [ClassData(typeof(CadCamCoordinatesValidData))]
    public void SetCamCoordinates_ShouldPopulateProperly_WhenCoordinatesAreValid(Coordinates coordinates)
    {
        var cad = CreateCad();

        cad.SetCamCoordinates(coordinates);

        Assert.Equal(coordinates, cad.CamCoordinates);
    }

    [Theory]
    [ClassData(typeof(CadCamCoordinatesInvalidData))]
    public void SetCamCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid(Coordinates coordinates)
    {
        var cad = CreateCad();

        Assert.Throws<CustomValidationException<Cad>>(() =>
        {
            cad.SetCamCoordinates(coordinates);
        });
    }
}
