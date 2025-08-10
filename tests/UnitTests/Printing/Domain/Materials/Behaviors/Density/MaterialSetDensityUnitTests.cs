using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density;

using static MaterialsData;

public class MaterialSetDensityUnitTests : MaterialsBaseUnitTests
{
	[Fact]
	public void SetDensity_ShouldNotThrowException()
	{
		CreateMaterial().SetDensity(MaxValidDensity);
	}

	[Fact]
	public void SetDensity_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetDensity(MaxValidDensity);

		Assert.Equal(MaxValidDensity, material.Density);
	}

	[Theory]
	[ClassData(typeof(Data.MaterialSetDensityInvalidData))]
	public void SetDensity_ShouldThrowException_WhenDensityInvalid(decimal density)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(
			() => material.SetDensity(density)
		);
	}
}
