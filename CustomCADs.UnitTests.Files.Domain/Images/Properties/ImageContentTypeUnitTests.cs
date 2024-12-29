namespace CustomCADs.UnitTests.Files.Domain.Images.Properties;

public class ImageContentTypeUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [InlineData(ImageValidContentType2)]
    public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
    {
        var image = CreateImage();

        image.SetContentType(contentType);
    }

    [Theory]
    [InlineData(ImageValidContentType2)]
    public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
    {
        var image = CreateImage();

        image.SetContentType(contentType);

        Assert.Equal(contentType, image.ContentType);
    }

    [Theory]
    [InlineData(ImageInvalidContentType)]
    public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
    {
        var image = CreateImage();

        Assert.Throws<ImageValidationException>(() =>
        {
            image.SetContentType(contentType);
        });
    }
}
