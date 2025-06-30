using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.Volume;

using static CadsData;

public class CadVolumeUnitTests : CadsBaseUnitTests
{
	[Fact]
	public void SetKey_ShouldNotThrowException_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetVolume(ValidVolume);
	}

	[Fact]
	public void SetKey_ShouldPopulateProperties_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetVolume(ValidVolume);

		Assert.Equal(ValidVolume, cad.Volume);
	}

	[Fact]
	public void SetKey_ShouldThrowException_WhenKeyIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetVolume(InvalidVolume)

		);
	}
}
