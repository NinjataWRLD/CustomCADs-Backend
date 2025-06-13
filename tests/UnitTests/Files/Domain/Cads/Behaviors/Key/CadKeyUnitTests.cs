using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.Key;

using static CadsData;

public class CadKeyUnitTests : CadsBaseUnitTests
{
	[Fact]
	public void SetKey_ShouldNotThrowException_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetKey(ValidKey);
	}

	[Fact]
	public void SetKey_ShouldPopulateProperly_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetKey(ValidKey);

		Assert.Equal(ValidKey, cad.Key);
	}

	[Fact]
	public void SetKey_ShouldThrowException_WhenKeyIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetKey(InvalidKey)
		);
	}
}
