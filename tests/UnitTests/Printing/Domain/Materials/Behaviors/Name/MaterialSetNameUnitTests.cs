using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name;

using static MaterialsData;

public class MaterialSetNameUnitTests : MaterialsBaseUnitTests
{
	[Fact]
	public void SetName_ShouldNotThrowException()
	{
		CreateMaterial().SetName(MaxValidName);
	}

	[Fact]
	public void SetName_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetName(MaxValidName);

		Assert.Equal(MaxValidName, material.Name);
	}

	[Theory]
	[ClassData(typeof(Data.MaterialSetNameInvalidData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(
			() => material.SetName(name)
		);
	}
}
