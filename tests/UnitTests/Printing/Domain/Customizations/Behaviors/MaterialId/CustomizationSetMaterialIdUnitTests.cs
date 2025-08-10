using CustomCADs.Printing.Domain.Customizations;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.MaterialId;

using static CustomizationsData;

public class CustomizationSetMaterialIdUnitTests : CustomizationsBaseUnitTests
{
	[Fact]
	public void SetMaterialId_ShouldNotThrowException()
	{
		CreateCustomization().SetMaterialId(ValidMaterialId);
	}

	[Fact]
	public void SetMaterialId_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetMaterialId(ValidMaterialId);

		Assert.Equal(ValidMaterialId, material.MaterialId);
	}
}
