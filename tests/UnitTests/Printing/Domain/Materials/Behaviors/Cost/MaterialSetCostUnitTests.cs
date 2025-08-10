using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Cost;

using static MaterialsData;

public class MaterialSetCostUnitTests : MaterialsBaseUnitTests
{
	[Fact]
	public void SetCost_ShouldNotThrowException()
	{
		CreateMaterial().SetCost(MaxValidCost);
	}

	[Fact]
	public void SetCost_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetCost(MaxValidCost);

		Assert.Equal(MaxValidCost, material.Cost);
	}

	[Theory]
	[ClassData(typeof(Data.MaterialSetCostInvalidData))]
	public void SetCost_ShouldThrowException_WhenCostInvalid(decimal cost)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(
			() => material.SetCost(cost)
		);
	}
}
