using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.ContentType.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.ContentType;

public class CadContentTypeUnitTests : CadsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CadContentTypeValidData))]
	public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
	{
		var cad = CreateCad();

		cad.SetContentType(contentType);
	}

	[Theory]
	[ClassData(typeof(CadContentTypeValidData))]
	public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
	{
		var cad = CreateCad();

		cad.SetContentType(contentType);

		Assert.Equal(contentType, cad.ContentType);
	}

	[Theory]
	[ClassData(typeof(CadContentTypeInvalidData))]
	public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(() =>
		{
			cad.SetContentType(contentType);
		});
	}
}
