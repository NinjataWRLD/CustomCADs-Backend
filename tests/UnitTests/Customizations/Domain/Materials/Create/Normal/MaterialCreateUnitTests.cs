using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customizations.Domain.Materials.Create.Normal;

using static MaterialsData;

public class MaterialCreateUnitTests : MaterialsBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateMaterial(MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		Material material = CreateMaterial(MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId);

		Assert.Multiple(
			() => Assert.Equal(MaxValidName, material.Name),
			() => Assert.Equal(MaxValidDensity, material.Density),
			() => Assert.Equal(MaxValidCost, material.Cost),
			() => Assert.Equal(ValidTextureId, material.TextureId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.MaterialCreateInvalidData))]
	public void Create_ShouldThrowExcetion_WhenInvalid(string name, decimal density, decimal cost)
	{
		Assert.Throws<CustomValidationException<Material>>(
			() => CreateMaterial(name, density, cost, ValidTextureId)
		);
	}
}
