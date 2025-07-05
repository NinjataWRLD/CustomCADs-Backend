using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customizations.Domain.Customizations.Behaviors.Scale;

using static CustomizationsData;

public class CustomizationSetScaleUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void SetScale_ShouldNotThrowException()
	{
		CreateCustomization().SetScale(MaxValidScale);
	}

	[Fact]
	public void SetScale_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetScale(MaxValidScale);

		Assert.Equal(MaxValidScale, material.Scale);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationSetScaleInvalidData))]
	public void SetScale_ShouldThrowException_WhenScaleInvalid(decimal scale)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetScale(scale)
		);
	}
}
