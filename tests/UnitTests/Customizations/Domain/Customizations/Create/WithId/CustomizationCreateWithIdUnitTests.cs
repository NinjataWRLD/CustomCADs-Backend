using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customizations.Domain.Customizations.Create.WithId;

using static CustomizationsData;

public class CustomizationCreateWithIdUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowException()
	{
		CreateCustomizationWithId(ValidId, MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties()
	{
		Customization customization = CreateCustomizationWithId(ValidId, MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, customization.Id),
			() => Assert.Equal(MaxValidScale, customization.Scale),
			() => Assert.Equal(MaxValidInfill, customization.Infill),
			() => Assert.Equal(MaxValidVolume, customization.Volume),
			() => Assert.Equal(ValidColor, customization.Color),
			() => Assert.Equal(ValidMaterialId, customization.MaterialId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenInvalid(decimal scale, decimal infill, decimal volume, string color)
	{
		Assert.Throws<CustomValidationException<Customization>>(
			() => CreateCustomizationWithId(ValidId, scale, infill, volume, color, ValidMaterialId)
		);
	}
}
