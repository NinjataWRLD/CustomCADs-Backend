﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName.Data;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

public class CustomSetNameUnitTests : CustomsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CustomSetNameValidData))]
    public void SetName_ShouldNotThrowException_WhenCustomValid(string name)
    {
        CreateCustom().SetName(name);
    }

    [Theory]
    [ClassData(typeof(CustomSetNameValidData))]
    public void SetName_ShouldPopulateProperly(string name)
    {
        var Custom = CreateCustom();
        Custom.SetName(name);
        Assert.Equal(name, Custom.Name);
    }

    [Theory]
    [ClassData(typeof(CustomSetNameInvalidData))]
    public void SetName_ShouldThrowException_WhenNameInvalid(string name)
    {
        Assert.Throws<CustomValidationException<Custom>>(() =>
        {
            CreateCustom().SetName(name);
        });
    }
}
