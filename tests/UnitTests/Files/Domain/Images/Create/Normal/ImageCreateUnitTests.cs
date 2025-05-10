using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;

using Data;

public class ImageCreateUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ImageCreateValidData))]
    public void Create_ShouldNotThrowExcepion_WhenImageIsValid(string key, string contentType)
    {
        Image.Create(key, contentType);
    }

    [Theory]
    [ClassData(typeof(ImageCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenImageIsValid(string key, string contentType)
    {
        var image = Image.Create(key, contentType);

        Assert.Multiple(
            () => Assert.Equal(key, image.Key),
            () => Assert.Equal(contentType, image.ContentType)
        );
    }

    [Theory]
    [ClassData(typeof(ImageCreateInvalidKeyData))]
    public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType)
    {
        Assert.Throws<CustomValidationException<Image>>(() =>
        {
            Image.Create(key, contentType);
        });
    }

    [Theory]
    [ClassData(typeof(ImageCreateInvalidContentTypeData))]
    public void Create_ShouldThrowException_WhenContentTypeIsInvalid(string key, string contentType)
    {
        Assert.Throws<CustomValidationException<Image>>(() =>
        {
            Image.Create(key, contentType);
        });
    }
}
