using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId;

using static ImagesData;

public class ImageCreateWithIdUnitTests : ImagesBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		CreateImageWithId(id: null, ValidKey, ValidContentType);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties_WhenImageIsValid()
	{
		var image = CreateImageWithId(id: null, ValidKey, ValidContentType);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, image.Key),
			() => Assert.Equal(ValidContentType, image.ContentType)
		);
	}

	[Theory]
	[ClassData(typeof(Data.ImageCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenImageIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(
			() => CreateImageWithId(id: null, key, contentType)
		);
	}
}
