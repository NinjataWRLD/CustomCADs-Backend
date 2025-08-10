using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Infill;

using static CustomizationsData;

public class CustomizationSetInfillUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void SetInfill_ShouldNotThrowException()
	{
		CreateCustomization().SetInfill(MaxValidInfill);
	}

	[Fact]
	public void SetInfill_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetInfill(MaxValidInfill);

		Assert.Equal(MaxValidInfill, material.Infill);
	}

	[Theory]
	[ClassData(typeof(Data.CustomizationSetInfillInvalidData))]
	public void SetInfill_ShouldThrowException_WhenInfillInvalid(decimal infill)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetInfill(infill)
		);
	}
}
