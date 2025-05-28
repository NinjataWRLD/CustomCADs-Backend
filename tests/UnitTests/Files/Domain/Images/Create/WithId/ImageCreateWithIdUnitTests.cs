using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId;

using Data;

public class ImageCreateWithIdUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ImageCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowExcepion_WhenImageIsValid(string key, string contentType)
    {
        Image.Create(key, contentType);
    }

    [Theory]
    [ClassData(typeof(ImageCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenImageIsValid(string key, string contentType)
    {
        var image = Image.Create(key, contentType);

        Assert.Multiple(
            () => Assert.Equal(key, image.Key),
            () => Assert.Equal(contentType, image.ContentType)
        );
    }

    [Theory]
    [ClassData(typeof(ImageCreateWithIdInvalidKeyData))]
    [ClassData(typeof(ImageCreateWithIdInvalidContentTypeData))]
    public void CreateWithId_ShouldThrowException_WhenImageIsInvalid(string key, string contentType)
    {
        Assert.Throws<CustomValidationException<Image>>(
            () => Image.Create(key, contentType)
        );
    }
}
