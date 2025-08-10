using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static CustomizationsData;

public class CustomizationSetVolumeUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void SetVolume_ShouldNotThrowException()
	{
		CreateCustomization().SetVolume(MaxValidVolume);
	}

	[Fact]
	public void SetVolume_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetVolume(MaxValidVolume);

		Assert.Equal(MaxValidVolume, material.Volume);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationSetVolumeInvalidData))]
	public void SetVolume_ShouldThrowException_WhenVolumeInvalid(decimal volume)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetVolume(volume)
		);
	}
}
