using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Color;

using static CustomizationsData;

public class CustomizationSetColorUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void SetColor_ShouldNotThrowException()
	{
		CreateCustomization().SetColor(ValidColor);
	}

	[Fact]
	public void SetColor_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetColor(ValidColor);

		Assert.Equal(ValidColor, material.Color);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationSetColorInvalidData))]
	public void SetColor_ShouldThrowException_WhenColorInvalid(string color)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetColor(color)
		);
	}
}
