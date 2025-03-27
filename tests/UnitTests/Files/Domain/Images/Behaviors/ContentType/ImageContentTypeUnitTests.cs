using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Files.Domain.Images.Behaviors.ContentType.Data;

namespace CustomCADs.UnitTests.Files.Domain.Images.Behaviors.ContentType;

public class ImageContentTypeUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ImageContentTypeValidData))]
    public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
    {
        var image = CreateImage();

        image.SetContentType(contentType);
    }

    [Theory]
    [ClassData(typeof(ImageContentTypeValidData))]
    public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
    {
        var image = CreateImage();

        image.SetContentType(contentType);

        Assert.Equal(contentType, image.ContentType);
    }

    [Theory]
    [ClassData(typeof(ImageContentTypeInvalidData))]
    public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
    {
        var image = CreateImage();

        Assert.Throws<CustomValidationException<Image>>(() =>
        {
            image.SetContentType(contentType);
        });
    }
}
