using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.ContentType;

using static CadsData;

public class CadContentTypeUnitTests : CadsBaseUnitTests
{
	[Fact]
	public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid()
	{
		var cad = CreateCad();

		cad.SetContentType(ValidContentType);
	}

	[Fact]
	public void SetContentType_ShouldPopulateProperties_WhenContentTypeIsValid()
	{
		var cad = CreateCad();

		cad.SetContentType(ValidContentType);

		Assert.Equal(ValidContentType, cad.ContentType);
	}

	[Fact]
	public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetContentType(InvalidContentType)
		);
	}
}
