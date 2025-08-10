using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create.Normal;

using static CustomizationsData;

public class CustomizationCreateUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateCustomization(MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		Customization customization = CreateCustomization(MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);

		Assert.Multiple(
			() => Assert.Equal(MaxValidScale, customization.Scale),
			() => Assert.Equal(MaxValidInfill, customization.Infill),
			() => Assert.Equal(MaxValidVolume, customization.Volume),
			() => Assert.Equal(ValidColor, customization.Color),
			() => Assert.Equal(ValidMaterialId, customization.MaterialId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationCreateInvalidData))]
	public void Create_ShouldThrowException_WhenInvalid(decimal scale, decimal infill, decimal volume, string color)
	{
		Assert.Throws<CustomValidationException<Customization>>(
			() => CreateCustomization(scale, infill, volume, color, ValidMaterialId)
		);
	}
}
