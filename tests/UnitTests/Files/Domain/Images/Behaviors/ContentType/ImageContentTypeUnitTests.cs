using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Images.Behaviors.ContentType;

using static ImagesData;

public class ImageContentTypeUnitTests : ImagesBaseUnitTests
{
	[Fact]
	public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid()
	{
		var image = CreateImage();

		image.SetContentType(ValidContentType);
	}

	[Fact]
	public void SetContentType_ShouldPopulateProperties_WhenContentTypeIsValid()
	{
		var image = CreateImage();

		image.SetContentType(ValidContentType);

		Assert.Equal(ValidContentType, image.ContentType);
	}

	[Fact]
	public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid()
	{
		var image = CreateImage();

		Assert.Throws<CustomValidationException<Image>>(
			() => image.SetContentType(InvalidContentType)
		);
	}
}
