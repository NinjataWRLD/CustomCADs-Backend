using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;

using static ImagesData;

public class ImageCreateUnitTests : ImagesBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		Image.Create(ValidKey, ValidContentType);
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenImageIsValid()
	{
		var image = Image.Create(ValidKey, ValidContentType);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, image.Key),
			() => Assert.Equal(ValidContentType, image.ContentType)
		);
	}

	[Theory]
	[ClassData(typeof(Data.ImageCreateInvalidData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(
			() => Image.Create(key, contentType)
		);
	}
}
