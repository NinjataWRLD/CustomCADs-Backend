using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Files.Domain.Images.Create.Normal.Data;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;

public class ImageCreateUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ImageCreateWithIdValidData))]
    public void Create_ShouldNotThrowExcepion_WhenImageIsValid(string key, string contentType)
    {
        Image.Create(key, contentType);
    }

    [Theory]
    [ClassData(typeof(ImageCreateWithIdValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenImageIsValid(string key, string contentType)
    {
        var image = Image.Create(key, contentType);

        Assert.Multiple(
            () => Assert.Equal(key, image.Key),
            () => Assert.Equal(contentType, image.ContentType)
        );
    }

    [Theory]
    [ClassData(typeof(ImageCreateWithIdInvalidKeyData))]
    public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType)
    {
        Assert.Throws<CustomValidationException<Image>>(() =>
        {
            Image.Create(key, contentType);
        });
    }

    [Theory]
    [ClassData(typeof(ImageCreateWithIdInvalidContentTypeData))]
    public void Create_ShouldThrowException_WhenContentTypeIsInvalid(string key, string contentType)
    {
        Assert.Throws<CustomValidationException<Image>>(() =>
        {
            Image.Create(key, contentType);
        });
    }
}
