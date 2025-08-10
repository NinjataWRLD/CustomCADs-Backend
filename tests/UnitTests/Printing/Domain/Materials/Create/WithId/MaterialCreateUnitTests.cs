using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;


namespace CustomCADs.UnitTests.Printing.Domain.Materials.Create.WithId;

using static MaterialsData;

public class MaterialCreateUnitTests : MaterialsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowException()
	{
		CreateMaterialWithId(ValidId, MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties()
	{
		Material material = CreateMaterialWithId(ValidId, MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, material.Id),
			() => Assert.Equal(MaxValidName, material.Name),
			() => Assert.Equal(MaxValidDensity, material.Density),
			() => Assert.Equal(MaxValidCost, material.Cost),
			() => Assert.Equal(ValidTextureId, material.TextureId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.MaterialCreateInvalidData))]
	public void CreateWithId_ShouldThrowExcetion_WhenInvalid(string name, decimal density, decimal cost)
	{
		Assert.Throws<CustomValidationException<Material>>(
			() => CreateMaterialWithId(ValidId, name, density, cost, ValidTextureId)
		);
	}
}
