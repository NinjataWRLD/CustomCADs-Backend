﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using Data;

public class CustomSetDescriptionUnitTests : CustomsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CustomSetDescriptionValidData))]
	public void SetDescription_ShouldNotThrowException_WhenCustomValid(string description)
	{
		CreateCustom().SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(CustomSetDescriptionValidData))]
	public void SetDescription_ShouldPopulateProperties(string description)
	{
		var Custom = CreateCustom();
		Custom.SetDescription(description);
		Assert.Equal(description, Custom.Description);
	}

	[Theory]
	[ClassData(typeof(CustomSetDescriptionInvalidData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom().SetDescription(description)
		);
	}
}
