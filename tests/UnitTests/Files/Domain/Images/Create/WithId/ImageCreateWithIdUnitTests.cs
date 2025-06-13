using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId;

using static ImagesData;

public class ImageCreateWithIdUnitTests : ImagesBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		Image.Create(ValidKey, ValidContentType);
	}

	[Fact]
	public void CreateWithId_ShouldPopulatePropertiesProperly_WhenImageIsValid()
	{
		var image = Image.Create(ValidKey, ValidContentType);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, image.Key),
			() => Assert.Equal(ValidContentType, image.ContentType)
		);
	}

	[Theory]
	[ClassData(typeof(Data.ImageCreateWithIdInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenImageIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(
			() => Image.Create(key, contentType)
		);
	}
}
